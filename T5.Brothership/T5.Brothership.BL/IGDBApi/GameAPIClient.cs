using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.IGDBApi
{
    public class GameAPIClient : IDisposable, IGameAPIService
    {//TODO(Dave) Make Game service/ API Client naming consistant
        private readonly HttpClient client = new HttpClient();

        public GameAPIClient()
        {
            client = CreateClient();
        }

        public void Dispose()
        {
            client.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            var igdbGame = await GetGameFromAPI(id);

            var game = new Game
            {
                igdbID = igdbGame.id,
                Title = igdbGame.name,
                ImgCloudinaryId = igdbGame.cover.cloudinary_id
            };

            return game;
        }
        public async Task<List<Game>> SearchByTitleAsync(string gameName, int limit = 10, int page = 0)
        {
            var response = await SearchAPI(gameName, limit, page * limit).ConfigureAwait(false);
            return CreateGamesFromResponse(response);
        }
        private HttpClient CreateClient()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-Mashape-Key", GameApiCredentials.API_KEY);
            client.BaseAddress = new Uri("https://igdbcom-internet-game-database-v1.p.mashape.com/' ");

            return client;
        }

        private List<GameCategory> CreateGameCategories(IGDBGame game)
        {
            var gameCategories = new List<GameCategory>();

            foreach (var categoryId in game.genres)
            {
                gameCategories.Add(new GameCategory { ID = categoryId });
            }

            return gameCategories;
        }

        private List<Game> CreateGamesFromResponse(List<IGDBGame> response)
        {
            var games = new List<Game>();

            foreach (var game in response)
            {
                Game newgame = new Game
                {
                    igdbID = game.id,
                    Title = game.name,
                    ImgCloudinaryId = game.cover == null ? string.Empty : game.cover.cloudinary_id
                };

                if (game.genres != null)
                {
                    newgame.GameCategories = CreateGameCategories(game);
                }
                games.Add(newgame);
            }
            return games;
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

        private async Task<IGDBGame> GetGameFromAPI(int id)
        {
            string[] fields = { "name", "cover" };
            var responseMessage = await client.GetAsync("games/" + id + "?fields=" + GetFieldsString(fields));
            var json = responseMessage.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<List<IGDBGame>>(json);
            var igdbGame = response[0];
            return igdbGame;
        }

        private async Task<List<IGDBGame>> SearchAPI(string gameName, int limit, int offset)
        {
            string[] fields = { "name", "cover", "genres" };

            var responseMessage = await client.GetAsync("games/?fields=" + GetFieldsString(fields) + "&limit=" + limit
                                                                        + "&offset=" + offset + "&search=" + gameName).ConfigureAwait(false);
            var json = responseMessage.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<List<IGDBGame>>(json);
            return response;
        }
    }
}
