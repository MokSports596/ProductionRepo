using MokSportsApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Interfaces
{
    public interface IGameService
    {
        Task<Game?> GetGameByIdAsync(int id);
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<IEnumerable<Game>> GetGamesByDateAsync(DateTime date);
        Task<IEnumerable<Game>> GetGamesByTeamAsync(string teamName);
    }
}
