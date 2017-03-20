using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;
using System.IO;
using System.Linq;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{
    [TestClass]
    public class GameRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var dbContext = new brothershipEntities())
            {
                SqlScriptRunner.RunAddTestDataScript(dbContext);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Add_WasDataAdded_ActualEqualsExpected()
        {
            var expectedGame = new Game
            {
                GameCategories = new List<GameCategory> { new GameCategory { ID = 12, Description = "Role-playing (RPG)" } },
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
        public void DeleteByEntity_WasDeleted_ActualGameIsNull()
        {
            Game actualGame;
            var typeToDelete = AddandGetTestGame();

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                gameRepo.Delete(typeToDelete);
                gameRepo.SaveChanges();
                actualGame = gameRepo.GetById(typeToDelete.ID);
            }

            Assert.IsNull(actualGame);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_ActualGameDataIsNull()
        {
            var typeIdToDelete = AddandGetTestGame().ID;
            Game actualGame;

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                Game game = gameRepo.GetById(typeIdToDelete);
                game.GameCategories.Clear();
                gameRepo.Delete(typeIdToDelete);
                gameRepo.SaveChanges();
                actualGame = gameRepo.GetById(typeIdToDelete);
                Assert.IsNull(actualGame);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_AllGamesReturned_CountEqualsActual()
        {
            const int expectedCount = 45;
            int actualCount;

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                actualCount = gameRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_ActualEqualsExpected()
        {
            var expectedGame = new Game
            {
                ID = 4,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 31, Description = "Adventure" } },
                igdbID = null,
                Title = "Fallout 4",
            };
            Game actualGame;

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                actualGame = gameRepo.GetById(expectedGame.ID);
                AssertGamesEqual(expectedGame, actualGame);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_WasGameUpdatad_ExpectedEqualsActual()
        {
            var expectedGame = new Game
            {
                ID = 2,
                igdbID = 3452354,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 312 } },
                Title = "Test Updated Title"
            };
            Game actualGame;

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                gameRepo.Update(expectedGame);
                gameRepo.SaveChanges();
                actualGame = gameRepo.GetById(expectedGame.ID);
            }

            AssertGamesEqual(expectedGame, actualGame);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetByIgdbId_WasCorrectGameGot_ExpectedDataEqualsActual()
        {
            var expectedGame = new Game
            {
                ID = 40,
                igdbID = 1277,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 11, Description = "Real Time Strategy (RTS)" } },
                Title = "Plants vs. Zombies"
            };
            Game actualGame;

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                gameRepo.GetByIgdbId((int)expectedGame.igdbID);
                gameRepo.SaveChanges();
                actualGame = gameRepo.GetById(expectedGame.ID);
                AssertGamesEqual(expectedGame, actualGame);
            }
        }

        private Game AddandGetTestGame()
        {
            var game = new Game
            {
                igdbID = 23434,
                Title = "Test Game",
            };
            game.GameCategories.Add(new GameCategory { ID = 11, Description = "Real Time Strategy (RTS)"});

            using (var gameRepo = new GameRepository(new brothershipEntities()))
            {
                gameRepo.Add(game);
                gameRepo.SaveChanges();
            }

            return game;
        }

        private void AssertGamesEqual(Game expected, Game actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.igdbID, actual.igdbID);
            Assert.AreEqual(expected.Title, actual.Title);

            foreach (var category in expected.GameCategories)
            {
                Assert.IsTrue(category.ID == actual.GameCategories.FirstOrDefault(p => p.ID == category.ID).ID);
                Assert.IsTrue(category.Description == actual.GameCategories.FirstOrDefault(p => p.ID == category.ID).Description);
            }
        }
    }
}
