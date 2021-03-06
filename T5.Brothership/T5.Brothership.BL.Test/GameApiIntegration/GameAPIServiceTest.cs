﻿using System;
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
    public class GameAPIClientTest
    {
        [TestMethod, TestCategory("IntegrationTest")]
        public async Task SearchGamesAsync_DidRetrieveGames_CountGreaterThanOne()
        {
            const string searchTerm = "Zelda";
            var games = new List<Game>();
            using (var gameService = new GameAPIClient())
            {
                games = await gameService.SearchByTitleAsync(searchTerm);
            }
            foreach (var game in games)
            {
                Debug.WriteLine("ID =" + game.igdbID + " Title =" + game.Title);
            }
            Assert.IsTrue(games.Count >= 1);
        }


        [TestMethod, TestCategory("IntegrationTest")]
        public async Task SearchGamesAsync_OnlyRetrievedGamesWithSearchTerm_AllGamesContainSearchTerm()
        {
            const string searchTerm = "Zelda";
            var games = new List<Game>();

            using (var gameService = new GameAPIClient())
            {
                games = await gameService.SearchByTitleAsync(searchTerm);
            }

            foreach (var game in games)
            {
                Assert.IsTrue(game.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            Assert.IsTrue(games.Count >= 1);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public async Task GetById_GetCorrectGame_ExpectedGameEqualsActual()
        {
            const int expectedAPIId = 8534;
            const string expectedTitle = "Zelda's Adventure";

            var actualGame = new Game();

            using (var gameService = new GameAPIClient())
            {
                actualGame = await gameService.GetByIdAsync(expectedAPIId);
            }

            Assert.AreEqual(expectedAPIId, actualGame.igdbID, null, "Expected ID:" + expectedAPIId + "Actual ID:" + actualGame.igdbID);
            Assert.AreEqual(expectedTitle, actualGame.Title, null, "Expected Title:" + expectedTitle + "Actual Title:" + actualGame.Title);
        }
    }
}