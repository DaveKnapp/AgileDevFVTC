using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.IGDBApi;
using T5.Brothership.Entities.Models;
using System.Threading.Tasks;
using System.Diagnostics;

namespace T5.Brothership.BL.Test.GameApi
{
    [TestClass]
    public class GameAPIServiceTest
    {
        [TestMethod, TestCategory("IntegrationTest")]
        public async Task SearchGamesAsync_DidRetrieveGames_CountGreaterThanOne()
        {
            const string searchTerm = "Zelda";
            var games = new List<Game>();
            using (var gameService = new GameAPIService())
            {
                games = await gameService.SearchGamesAsync(searchTerm);
            }
            foreach (var game in games)
            {
                Debug.WriteLine(game.Title);
            }
            Assert.IsTrue(games.Count >= 1);
        }


        [TestMethod, TestCategory("IntegrationTest")]
        public async Task SearchGamesAsync_OnlyRetrievedGamesWithSearchTerm_AllGamesContainSearchTerm()
        {
            const string searchTerm = "Zelda";
            var games = new List<Game>();

            using (var gameService = new GameAPIService())
            {
                games = await gameService.SearchGamesAsync(searchTerm);
            }

            foreach (var game in games)
            {
                Assert.IsTrue(game.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            Assert.IsTrue(games.Count >= 1);
        }
    }
}
