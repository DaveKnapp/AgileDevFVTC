﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.IGDBApi
{//TODO Is this the best name?
    public class GameAPIService : IDisposable
    {
        private readonly HttpClient client = new HttpClient();

        //TODO Change game table in database to include more fields for GameAPI data like  CoverImageURL
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
        {
            string[] fields = { "name", "cover" };

            var responseMessage = await client.GetAsync("games/?fields=" + GetFieldsString(fields) + "&limit=" + limit
                                                                        + "&offset=" + offset + "&search=" + gameName);
            var json = responseMessage.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<List<IGDBGame>>(json);

            var games = new List<Game>();

            foreach (var game in response)
            {
                games.Add(new Game
                {
                    igdbID = game.id,
                    Title = game.name,
                });
            }
            return games;
        }

        public async Task<Game> GetById(int id)
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
