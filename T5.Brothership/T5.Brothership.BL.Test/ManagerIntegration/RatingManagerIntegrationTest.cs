using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Managers;
using T5.Brothership.PL;
using T5.Brothership.PL.Test;

namespace T5.Brothership.BL.Test.ManagerIntegration
{
    [TestClass]
    public class RatingManagerIntegrationTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var dbContext = DataContextCreator.CreateTestContext())
            {
                SqlScriptRunner.RunAddTestDataScript(dbContext);
            }
        }

        [TestMethod ,TestCategory("IntegrationTest")]
        public void GetAll_AllRatingsReturned_ExpectedCountEqualAcual()
        {
            using (RatingManager ratingManger = new RatingManager(new BrothershipUnitOfWork(DataContextCreator.CreateTestContext())))
            {
                const int expectedCount = 5;
                int actualCount = ratingManger.GetAll().Count;

                Assert.AreEqual(expectedCount, actualCount);
            }
        }
    }
}
