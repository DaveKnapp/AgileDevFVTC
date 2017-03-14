using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL;
using T5.Brothership.BL.IGDBApi;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Managers
{
    public class GameManager : IDisposable
    {
        IBrothershipUnitOfWork _unitOfWork = new BrothershipUnitOfWork();
        IGameAPIService _gameApiService = new GameAPIService();

        public GameManager()
        {
        }

        public GameManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GameManager(IBrothershipUnitOfWork unitOfWork, IGameAPIService gameApiService)
        {
            _gameApiService = gameApiService;
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _gameApiService.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<Game> GetByIgdbIds(int[] gameIds)
        {
            var games = new List<Game>();
            foreach (var id in gameIds)
            {
                //TODO(Dave)Make repo method to return multiple games from id
                _unitOfWork.Games.GetByIgdbId(id);
            }
            return games;
        }

        public async Task AddGameIfNotExistAsync(int igdbID)
        {
            if (!(_unitOfWork.Games.GetByIgdbId((int)igdbID) == null))
            {
                await AddGameToDatabase(igdbID);
            }
        }

        public async Task AddGamesIfNotExistsAsync(int[] igdbIDs)
        {
            foreach (var gameId in igdbIDs)
            {
                await AddGameIfNotExistAsync(gameId);
            }
        }

        private async Task AddGameToDatabase(int igdbID)
        {
            var game = await _gameApiService.GetByIdAsync((int)igdbID);
            _unitOfWork.Games.Add(game);
            _unitOfWork.Games.SaveChanges();
        }
    }
}
