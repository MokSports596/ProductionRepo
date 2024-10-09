using Microsoft.EntityFrameworkCore;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;

namespace MokSportsApp.Services.Implementations
{
    public class SeasonService : ISeasonService
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonService(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        public async Task AddSeason(Season season)
        {
            await _seasonRepository.AddSeason(season);
        }

        public async Task<List<Season>> GetAllSeasons()
        {
            return await _seasonRepository.GetAllSeasons();
        }

        public Task<Season> GetSeason(int id)
        {
            return _seasonRepository.GetSeason(id);
        }

        public async Task<Season> GetActiveSeason()
        {
            return await _seasonRepository.GetActiveSeason();
        }

        public Task<Season> GetSeasonByName(string name)
        {
            return _seasonRepository.GetSeasonByName(name);
        }

        public async Task UpdateSeason(Season season)
        {
            await _seasonRepository.UpdateSeason(season);
        }

        public async Task<bool> CheckActiveSeason()
        {
            return await _seasonRepository.CheckActiveSeason();
        }

        public void ValidateSeasonName(string name)
        {
            int year = DateTime.Now.Year;

            if (!int.TryParse(name, out _))
                throw new InvalidOperationException($"Season name should be a year e.g {year}");

            if (int.Parse(name) < year)
                throw new InvalidOperationException("Cannot create past seasons");
        }

        public void ValidateSeasonStatus(int value)
        {
            // Get the maximum value of the enum
            int maxEnumValue = (int)Enum.GetValues(typeof(SeasonStatus)).Cast<int>().Max();

            // Check for negative values
            if (value < 0)
            {
                throw new InvalidOperationException($"Invalid season status given");
            }

            // Check if the value exceeds the maximum enum value
            if (value > maxEnumValue)
            {
                throw new InvalidOperationException($"Invalid season status given");
            }
        }
    }
}