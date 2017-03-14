using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.IGDBApi;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test
{
    internal class GameApiServiceFake : IGameAPIService
    {
        List<Game> _fakeGames = new List<Game>();

        internal GameApiServiceFake()
        {
            InitializeGames();
        }

        public void Dispose()
        {
            _fakeGames = null;
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            await Task.Delay(1);
            return _fakeGames.FirstOrDefault(p => p.igdbID == id);

        }

        public async Task<List<Game>> SearchByTitleAsync(string gameName, int limit = 10, int offset = 0)
        {
            await Task.Delay(1);
            return _fakeGames.Where(p => p.Title.ToLower().Contains(gameName)).ToList();
        }

        private void InitializeGames()
        {
            _fakeGames.Add(new Game
            {
                //TODO Add data
            });
        }
    }
}
