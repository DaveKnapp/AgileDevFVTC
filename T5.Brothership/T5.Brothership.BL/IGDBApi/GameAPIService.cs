using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.IGDBApi
{//TODO Is this the best name?
    public class GameAPIService : IDisposable, IGameAPIService
    {
        private readonly HttpClient client = new HttpClient();

        public GameAPIService()
        {
            client = CreateClient();
        }

        public void Dispose()
        {
            client.Dispose();
            GC.SuppressFinalize(this);
        }
        private HttpClient CreateClient()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-Mashape-Key", GameApiCredentials.API_KEY);
            client.BaseAddress = new Uri("https://igdbcom-internet-game-database-v1.p.mashape.com/' ");

            return client;
        }
        public async Task<List<Game>> SearchByTitleAsync(string gameName, int limit = 10, int offset = 0)
        {//TODO(Dave) Refacotr
            string[] fields = { "name", "cover", "genres" };

            var responseMessage = await client.GetAsync("games/?fields=" + GetFieldsString(fields) + "&limit=" + limit
                                                                        + "&offset=" + offset + "&search=" + gameName);
            var json = responseMessage.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<List<IGDBGame>>(json);

            var games = new List<Game>();

            foreach (var game in response)
            {
                Game newgame = new Game
                {
                    igdbID = game.id,
                    Title = game.name,
                };

                foreach (var categoryId in game.genres)
                {
                    newgame.GameCategories.Add(new GameCategory { ID = categoryId });
                }

                games.Add(newgame);
            }
            return games;
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            string[] fields = { "name", "cover" };
            var responseMessage = await client.GetAsync("games/" + id + "?fields=" + GetFieldsString(fields));
            var json = responseMessage.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<List<IGDBGame>>(json);
            var igdbGame = response[0];

            var game = new Game
            {
                igdbID = igdbGame.id,
                Title = igdbGame.name,
            };

            return game;
        }

        private string GetFieldsString(string[] fields)
        {
            var builder = new StringBuilder();

            foreach (var field in fields)
            {
                builder.Append(field);
                if (fields.Last() != field)
                {
                    builder.Append(",");
                }
            }

            return builder.ToString();
        }
    }
}
