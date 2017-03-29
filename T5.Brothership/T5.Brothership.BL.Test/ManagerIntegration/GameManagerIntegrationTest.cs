using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Managers;
using T5.Brothership.PL;
using T5.Brothership.PL.Test;
using System.Linq;
using System.Threading.Tasks;

namespace T5.Brothership.BL.Test.ManagerIntegration
{
    [TestClass]
    public class GameManagerIntegrationTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var dbContext = DataContextCreator.CreateTestContext())
            {
                SqlScriptRunner.RunAddTestDataScript(dbContext);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetByIgdbIds_GamesInDataBaseReturn_ActualCountEqualsExpected()
        {
            var expectedIgdbIds = new int[] { 1331, 1277, 115 };

            using (var gameManager = new GameManager(new BrothershipUnitOfWork(DataContextCreator.CreateTestContext())))
            {
                var games = gameManager.GetByIgdbIds(expectedIgdbIds);
                Assert.AreEqual(expectedIgdbIds.Length, games.Count);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetByIgdbIds_GamesNotInDatabaseNotReturned_ActualCountEqualsExpected()
        {
            var expectedIgdbIdsInDb = new int[] { 1331, 1277, 115 };
            var igdbIdsNotInDB = new int[] { 434, 3432, 434, 4344 };

            var ids = new List<int>();

            ids.AddRange(expectedIgdbIdsInDb);
            ids.AddRange(igdbIdsNotInDB);

            using (var gameManager = new GameManager(new BrothershipUnitOfWork(DataContextCreator.CreateTestContext())))
            {
                var games = gameManager.GetByIgdbIds(ids.ToArray());

                foreach (var gameId in expectedIgdbIdsInDb)
                {
                    Assert.IsNotNull(games.FirstOrDefault(p => p.igdbID == gameId));
                }

                foreach (var gameId in igdbIdsNotInDB)
                {
                    Assert.IsNull(games.FirstOrDefault(p => p.igdbID == gameId));
                }
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public async Task AddGameIfNotExistAsync_NonExistingGameAdded_TotalGameCountIncreasedByOne()
        {
            using (var gameManager = new GameManager(new BrothershipUnitOfWork(DataContextCreator.CreateTestContext())))
            {
                const int gameIdToAdd = 5314;
                int gameCount = gameManager.GetAll().Count;

                await gameManager.AddGameIfNotExistAsync(gameIdToAdd);

                int newGameCount = gameManager.GetAll().Count;
                Assert.IsTrue(gameCount + 1 == newGameCount) ;
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public async Task AddGameIfNotExistAsync_ExistingGameNotAdded_TotalGameCountIsNotChanged()
        {
            using (var gameManager = new GameManager(new BrothershipUnitOfWork(DataContextCreator.CreateTestContext())))
            {
                const int gameIdToAdd = 1331;
                int gameCount = gameManager.GetAll().Count;

                await gameManager.AddGameIfNotExistAsync(gameIdToAdd);

                int newGameCount = gameManager.GetAll().Count;
                Assert.IsTrue(gameCount == newGameCount);
            }
        }
    }
}
