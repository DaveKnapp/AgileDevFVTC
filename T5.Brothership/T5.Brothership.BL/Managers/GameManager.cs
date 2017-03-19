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
        IBrothershipUnitOfWork _unitOfWork;
        IGameAPIService _gameApiService;

        public GameManager()
        {
            _unitOfWork = new BrothershipUnitOfWork();
            _gameApiService = new GameAPIService();

        }

        public GameManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _gameApiService = new GameAPIService();
        }

        public GameManager(IBrothershipUnitOfWork unitOfWork, IGameAPIService gameApiService)
        {
            _gameApiService = gameApiService;
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            _gameApiService?.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<Game> GetByIgdbIds(int[] gameIds)
        {
            var games = new List<Game>();

            foreach (var id in gameIds)
            {
                var game = _unitOfWork.Games.GetByIgdbId(id);

                if (game != null)
                {
                    games.Add(_unitOfWork.Games.GetByIgdbId(id));
                }
            }
            return games;
        }

        public async Task AddGameIfNotExistAsync(int igdbID)
        {
            if (_unitOfWork.Games.GetByIgdbId(igdbID) == null)
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
            var game = await _gameApiService.GetByIdAsync(igdbID);

            if (game != null)
            {
                _unitOfWork.Games.Add(game);
                _unitOfWork.Games.SaveChanges();
            }
        }
    }
}
