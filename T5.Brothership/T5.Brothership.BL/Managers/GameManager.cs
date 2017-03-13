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

        public GameManager()
        {
        }

        public GameManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }

        public Game AddGameByIgdbIdIfNotExist(int? igdbID)
        {

            throw new NotImplementedException();
            if (igdbID == null)
            {
                return null;
            }
     
        }

        public List<Game> AddGamesByIgdbIdIfNotExist(int[] igdbIDs)
        {
            throw new NotImplementedException();

            var games = new List<Game>();

            foreach (var gameId in igdbIDs)
            {
               Game game = AddGameByIgdbIdIfNotExist(gameId);
                if (game != null)
                {
                    games.Add(game);
                }
            }
            return games;
        }
    }
}
