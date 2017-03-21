using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.PL.Test;
using T5.Brothership.Entities.Models;
using T5.Brothership.BL.Managers;
using System.Threading.Tasks;
using System.Linq;

namespace T5.Brothership.BL.Test.ManagerUnitTests
{
    [TestClass]
    public class GameManagerUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public void GetByIgdbIds_WasDataGot_CountEqualsActual()
        {
            int[] expectedGameIds = { 523, 2342, 324, 43253 };

            List<Game> actualGames;
            using (var gameManager = new GameManager(new FakeBrothershipUnitOfWork(), new GameApiServiceFake()))
            {
                actualGames = gameManager.GetByIgdbIds(expectedGameIds);
            }
            Assert.AreEqual(expectedGameIds.Length, actualGames.Count);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void GetByIgdbIds_IdsNotExists_CountEqualsZero()
        {
            int[] expectedGameIds = { -234234, -312432, -32143, -21341234 };

            List<Game> actualGames;
            using (var gameManager = new GameManager(new FakeBrothershipUnitOfWork(), new GameApiServiceFake()))
            {
                actualGames = gameManager.GetByIgdbIds(expectedGameIds);
            }
            Assert.AreEqual(actualGames.Count, 0);
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task AddGameIfNotExistAsync_ExistedGameNotAdded_GameCountDoesntChange()
        {
            const int existedGameId = 1039;

            using (var fakeUnitOfWork = new FakeBrothershipUnitOfWork())
            {
                var expectedGameCount = fakeUnitOfWork.Games.GetAll().Count();

                using (var gameManager = new GameManager(fakeUnitOfWork, new GameApiServiceFake()))
                {
                    await gameManager.AddGameIfNotExistAsync(existedGameId);
                }

                var actualGameCount = fakeUnitOfWork.Games.GetAll().Count();
                Assert.AreEqual(expectedGameCount, actualGameCount);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task AddGameIfNotExistAsync_NonExistedGameAdded_GameCountIncreases()
        {
            const int addedGameId = 18017;

            using (var fakeUnitOfWork = new FakeBrothershipUnitOfWork())
            {
                var expectedGameCount = fakeUnitOfWork.Games.GetAll().Count();

                using (var gameManager = new GameManager(fakeUnitOfWork, new GameApiServiceFake()))
                {
                    await gameManager.AddGameIfNotExistAsync(addedGameId);
                }

                var actualGameCount = fakeUnitOfWork.Games.GetAll().Count();
                Assert.AreEqual(expectedGameCount + 1, actualGameCount);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task AddGamesIfNotExistAsync_ExistedGameNotAdded_GameCountDoesNotIncrease()
        {
            int[] existingGameIds = { 43253, 4325, 523, 324 };

            using (var fakeUnitOfWork = new FakeBrothershipUnitOfWork())
            {
                var expectedCount = fakeUnitOfWork.Games.GetAll().Count();

                using (var gameManager = new GameManager(fakeUnitOfWork, new GameApiServiceFake()))
                {
                    await gameManager.AddGamesIfNotExistsAsync(existingGameIds);
                }

                var actualGameCount = fakeUnitOfWork.Games.GetAll().Count();
                Assert.AreEqual(expectedCount, actualGameCount);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task AddGamesIfNotExistAsync_NonExistedGameAdded_GameCountIncreases()
        {
            int[] idsToAdd = { 2276, 4325, 534, 1039 };

            using (var fakeUnitOfWork = new FakeBrothershipUnitOfWork())
            {
                var expectedGameCount = fakeUnitOfWork.Games.GetAll().Count();

                using (var gameManager = new GameManager(fakeUnitOfWork, new GameApiServiceFake()))
                {
                    await gameManager.AddGamesIfNotExistsAsync(idsToAdd);
                }

                var actualGameCount = fakeUnitOfWork.Games.GetAll().Count();
                Assert.AreEqual(expectedGameCount + 2, actualGameCount);
            }
        }
    }
}
