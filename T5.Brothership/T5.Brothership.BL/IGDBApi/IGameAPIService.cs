using System.Collections.Generic;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.IGDBApi
{
    public interface IGameAPIService
    {
        void Dispose();
        Task<Game> GetByIdAsync(int id);
        Task<List<Game>> SearchByTitleAsync(string gameName, int limit = 10, int offset = 0);
    }
}