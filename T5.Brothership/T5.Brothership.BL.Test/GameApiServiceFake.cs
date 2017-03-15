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
               igdbID = 7346,
               Title = "The Legend of Zelda: Breath of the Wild",
               ImgCloudinaryId = "jk9el4ksl4c7qwaex2y5",
            });

            _fakeGames.Add(new Game
            {
                igdbID = 18017,
                Title = "The Legend of Zelda: Twilight Princess HD",
                ImgCloudinaryId = "klebtdw8yafmdcyc8ljl",
            });

            _fakeGames.Add(new Game
            {
                igdbID = 11194,
                Title = "The Legend of Zelda: Tri Force Heroes",
                ImgCloudinaryId = "dtj80vlbzqbywzcdcdiv",
            });

            _fakeGames.Add(new Game
            {
                igdbID = 8593,
                Title = "The Legend of Zelda: Majora's Mask 3D",
                ImgCloudinaryId = "zpy6tbjyuhdtg4gvjl4n",
            });

            _fakeGames.Add(new Game
            {
                igdbID = 5314,
                Title = "Hyrule Warriors",
                ImgCloudinaryId = "qucn5uwcxryxsgsyoco5",
            });

            _fakeGames.Add(new Game
            {
                igdbID = 2909,
                Title = "The Legend of Zelda: A Link Between Worlds",
                ImgCloudinaryId = "r9ezsk5yhljc83dfjeqc",
            });

            _fakeGames.Add(new Game
            {
                igdbID = 2276,
                Title = "The Legend of Zelda: The Wind Waker HD",
                ImgCloudinaryId = "xcivov9txngp8jyd0cyd",
            });

            _fakeGames.Add(new Game
            {
                igdbID = 1035,
                Title = "The Legend of Zelda: The Minish Cap",
                ImgCloudinaryId = "rtsxyofw5svqq8tixtru",
            });

            _fakeGames.Add(new Game
            {
                igdbID = 534,
                Title = "The Legend of Zelda: Skyward Sword",
                ImgCloudinaryId = "acupjubv0zatshopxcpn",
            });

            _fakeGames.Add(new Game
            {
                igdbID = 1039,
                Title = "The Legend of Zelda: Ocarina of Time 3D",
                ImgCloudinaryId = "y2nz1vsmqyrvm7twqrh0",
            });
        }
    }
}
