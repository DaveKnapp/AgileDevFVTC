using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    internal class GameFakeRepository : IGameRepository
    {
        List<Game> _fakeGames = new List<Game>();
        FakeBrothershipUnitOfWork _unitOfWork;

        public GameFakeRepository()
        {
            InitializeFakeGames();
        }

        public GameFakeRepository(FakeBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeFakeGames();
        }

        public void Add(Game entity)
        {
            entity.ID = GenerateGameId();
            _fakeGames.Add(entity);
        }

        public void Delete(int id)
        {
            var userType = _fakeGames.Single(p => p.ID == id);
            _fakeGames.Remove(userType);
        }

        public void Delete(Game entity)
        {
            var userType = _fakeGames.Single(p => p.ID == entity.ID);
            _fakeGames.Remove(userType);
        }

        public void Dispose()
        {
            _fakeGames = null;
        }

        public IQueryable<Game> GetAll()
        {
            return _fakeGames.AsQueryable();
        }

        public Game GetById(int id)
        {
            return _fakeGames.FirstOrDefault(p => p.ID == id);
        }

        public Game GetByIgdbId(int id)
        {
            return _fakeGames.FirstOrDefault(p => p.igdbID == id);
        }

        public void SaveChanges()
        {
        }

        public void Update(Game entity)
        {
            var gameIndex = _fakeGames.IndexOf(entity);
            _fakeGames[gameIndex] = entity;
        }

        private int GenerateGameId()
        {
            return _fakeGames.Max(p => p.ID) + 1;
        }

        private void InitializeFakeGames()
        {
            _fakeGames.Add(new Game
            {
                ID = 1,
                Title = "Street Fighter V",
                igdbID = 43253,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 4} }
            });

            _fakeGames.Add(new Game
            {
                ID = 2,
                Title = "Batlefield 1",
                igdbID = 4325,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 5 } }
            });

            _fakeGames.Add(new Game
            {
                ID = 3,
                Title = "Civilization V",
                igdbID = 523,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 11 } }
            });

            _fakeGames.Add(new Game
            {
                ID = 4,
                Title = "Resident Evil 7",
                igdbID = 324,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 31 } }
            });

            _fakeGames.Add(new Game
            {
                ID = 5,
                Title = "Madden 17",
                igdbID = 2342,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 13 } }
            });

            _fakeGames.Add(new Game
            {
                ID = 6,
                Title = "DOTA 2",
                igdbID = null,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 11} }
            });

            _fakeGames.Add(new Game
            {
                igdbID = 1039,
                Title = "The Legend of Zelda: Ocarina of Time 3D",
                ImgCloudinaryId = "y2nz1vsmqyrvm7twqrh0",
                GameCategories = new List<GameCategory> { new GameCategory { ID = 31 } }
            });
        }
    }
}
