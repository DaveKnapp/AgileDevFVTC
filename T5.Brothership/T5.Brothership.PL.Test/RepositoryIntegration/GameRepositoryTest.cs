﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;
using System.IO;
using System.Linq;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{
    /// <summary>
    /// Summary description for GameRepositoryTest
    /// </summary>
    [TestClass]
    public class GameRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            string script = File.ReadAllText(FilePaths.ADD_TEST_DATA_SCRIPT_PATH);

            using (brothershipEntities dataContext = new brothershipEntities())
            {
                dataContext.Database.ExecuteSqlCommand(script);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_Count_EqualActual()
        {
            int expectedCount = 45;
            int actualCount;

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                actualCount = gameRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_EqualExpectedData()
        {
            Game expectedGame = new Game
            {
                ID = 4,
                CategoryID = 2,
                igdbID = null,
                Title = "Fallout 4",
            };
            Game actualGame;

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                actualGame = gameRepo.GetById(expectedGame.ID);
            }

            AssertGamesEqual(expectedGame, actualGame);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Add_ActualAddedData_EqualsExpectedData()
        {
            Game expectedGame = new Game
            {
                CategoryID = 12,
                igdbID = 243242,
                Title = "Best Test Game",
            };
            Game actualGame;

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                gameRepo.Add(expectedGame);
                gameRepo.SaveChanges();
                actualGame = gameRepo.GetById(expectedGame.ID);
            }

            AssertGamesEqual(expectedGame, actualGame);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_ActualUpdatedData_EqualsExpectedData()
        {
            Game expectedGame = new Game
            {
                ID = 2,
                igdbID = 3452354,
                CategoryID = 12,
                Title = "Test Updated Title"
            };
            Game actualGame;

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                gameRepo.Update(expectedGame);
                actualGame = gameRepo.GetById(expectedGame.ID);
            }

            AssertGamesEqual(expectedGame, actualGame);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_actualDataNull()
        {
            Game actualGame;
            Game typeToDelete = AddandGetTestGame();

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                gameRepo.Delete(typeToDelete);
                gameRepo.SaveChanges();
                actualGame = gameRepo.GetById(typeToDelete.ID);
            }

            Assert.IsNull(actualGame);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_actualDataNull()
        {
            int typeIdToDelete = AddandGetTestGame().ID;
            Game actualGame;

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                gameRepo.Delete(typeIdToDelete);
                gameRepo.SaveChanges();
                actualGame = gameRepo.GetById(typeIdToDelete);
            }

            Assert.IsNull(actualGame);
        }

        private Game AddandGetTestGame()
        {
            var gameType = new Game
            {
                CategoryID = 11,
                igdbID = 23434,
                Title = "Test Game",
            };

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                gameRepo.Add(gameType);
                gameRepo.SaveChanges();
            }

            return gameType;
        }

        private void AssertGamesEqual(Game expected, Game actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.igdbID, actual.igdbID);
            Assert.AreEqual(expected.CategoryID, actual.CategoryID);
            Assert.AreEqual(expected.Title, actual.Title);
        }
    }
}