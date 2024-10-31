using Microsoft.EntityFrameworkCore;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.DTO;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Services.NotificationService;
using System.Diagnostics;

namespace MokSportsApp.Services.Implementations
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;

        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<List<Trade>> GetAllPendingTrades()
        {
            return await _tradeRepository.GetAllPendingTrades();
        }

        public async Task TradeTeams(TradeDTO tradeDTO)
        {

            var teamsHaveMatchToday = await _tradeRepository.CheckTodayTeamsMatches([tradeDTO.TeamIdToTrade, tradeDTO.TeamIdWithTrade]);

            if (teamsHaveMatchToday)
            {
                throw new InvalidOperationException("Trade not possible on team game for today");
            }

            var haveTeamsPlayedInWeek = await _tradeRepository.HaveTeamsPlayedInWeek([tradeDTO.TeamIdToTrade, tradeDTO.TeamIdWithTrade]);

            if (haveTeamsPlayedInWeek)
            {
                throw new InvalidOperationException("Trade not possible. Team(s) have played game in this week.");
            }

            if (!await _tradeRepository.IsFreeTeam(tradeDTO.TeamIdWithTrade, tradeDTO.LeagueId))
            {
                await TradeValidations(tradeDTO);

                var playerId = await _tradeRepository.GetPlayerUserId(tradeDTO.LeagueId, tradeDTO.TeamIdWithTrade);
                var deviceToken = await _tradeRepository.GetUserDeviceToken(playerId);

                if (string.IsNullOrEmpty(deviceToken)) throw new InvalidOperationException("User device is not registered");

                //Send notification to user device
                await FirebaseNotifications.SendPushNotificationAsync(deviceToken, "Trade request recieved", "A player want to do trade team with your team");

                tradeDTO.Status = TradeStatus.Pending;
                tradeDTO.TradedWithFreeTeam = false;

                await _tradeRepository.TradeTeams(tradeDTO);
            }
            else
            {
                tradeDTO.Status = TradeStatus.Accepted;
                tradeDTO.TradedWithFreeTeam = true;

                await _tradeRepository.TradeTeams(tradeDTO);

                await _tradeRepository.UpdateFranchise(tradeDTO.TeamIdToTrade, tradeDTO.LeagueId, tradeDTO.TeamIdWithTrade);

            }

        }

        public async Task UpdateTradeStatus(UpdateTradeStatusDTO input)
        {
            var trade = await GetTrade(input.TradeId);
            await _tradeRepository.UpdateTradeStatus(trade, input);

            if (input.Status == TradeStatus.Rejected)
            {
                var deviceToken = await _tradeRepository.GetUserDeviceToken(trade.CreatedBy);
                await FirebaseNotifications.SendPushNotificationAsync(deviceToken, "Trade Rejected", "League Players has rejected your trade request");
            }
        }


        public async Task<Trade> GetTrade(int tradeId)
        {
            var trade = await _tradeRepository.GetTrade(tradeId);

            if (trade == null)
            {
                throw new InvalidOperationException("Record not found");
            }

            return trade;
        }

        public async Task UpdateTradeRange(Trade[] trades)
        {
            await _tradeRepository.UpdateTradeRange(trades);
        }

        public async Task<List<TradeListDTO>> GetAllTrades(int userId)
        {
            return await _tradeRepository.GetAllTrades(userId);
        }

        private async Task TradeValidations(TradeDTO tradeDTO)
        {
            if (!await _tradeRepository.TeamToTradeIsAvailable(tradeDTO.TeamIdToTrade))
            {
                throw new InvalidOperationException("Base team already requested for another trade");
            }

            if (!await _tradeRepository.TeamWithTradeIsAvailable(tradeDTO.TeamIdWithTrade))
            {
                throw new InvalidOperationException("Team to trade with is already requested for another trade");
            }
        }

    }
}
