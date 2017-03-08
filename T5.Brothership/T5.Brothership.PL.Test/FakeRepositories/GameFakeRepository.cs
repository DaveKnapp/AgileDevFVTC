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

        public GameFakeRepository()
        {
            InitializeFakeGames();
        }

        public void Add(Game entity)
        {
            entity.ID = GenerateGameId();
            _fakeGames.Add(entity);
        }

        public void Delete(int id)
        {
            Game userType = _fakeGames.Single(p => p.ID == id);
            _fakeGames.Remove(userType);
        }

        public void Delete(Game entity)
        {
            Game userType = _fakeGames.Single(p => p.ID == entity.ID);
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

        public void SaveChanges()
        {
        }

        public void Update(Game entity)
        {
            int gameIndex = _fakeGames.IndexOf(entity);
            _fakeGames[gameIndex] = entity;
        }

        private int GenerateGameId()
        {
            return _fakeGames.Max(p => p.ID);
        }

        private void InitializeFakeGames()
        {
            _fakeGames.Add(new Game
            {
                ID = 1,
                Title = "Street Fighter V",
                igdbID = 43253,
                CategoryID = 1
            });

            _fakeGames.Add(new Game
            {
                ID = 2,
                Title = "Batlefield 1",
                igdbID = 4325,
                CategoryID = 4
            });

            _fakeGames.Add(new Game
            {
                ID = 3,
                Title = "Civilization V",
                igdbID = 523,
                CategoryID = 6
            });

            _fakeGames.Add(new Game
            {
                ID = 4,
                Title = "Resident Evil 7",
                igdbID = 324,
                CategoryID = 5
            });

            _fakeGames.Add(new Game
            {
                ID = 5,
                Title = "Madden 17",
                igdbID = 2342,
                CategoryID = 7
            });

            _fakeGames.Add(new Game
            {
                ID = 6,
                Title = "DOTA 2",
                igdbID = null,
                CategoryID = 11
            });

            _fakeGames.Add(new Game
            {
                ID = 7,
                Title = "Plants vs Zombies",
                igdbID = 8707,
                CategoryID = 14
            });
        }
    }
}
