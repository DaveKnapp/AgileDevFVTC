using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Managers;
using T5.Brothership.PL.Test;

namespace T5.Brothership.BL.Test.ManagerUnitTests
{
    [TestClass]
    public class RatingManagerTest
    {
        //TODO Add tests for
        //public List<UserRating> GetAllByUserId(int ratedUserId)
        //public UserRating GetById(int raterId, int userBeingRatedId)

        [TestMethod, TestCategory("UnitTest")]
        public void GetAverageRating_AverageCalulatedCorrectly_expectedAverageEqualsActual()
        {
            const double expectedAverage = 3.33;
            using (var ratingManager = new UserRatingManager(new FakeBrothershipUnitOfWork()))
            {
                double actualAverage = ratingManager.GetAverageRating(2);
                Assert.AreEqual(expectedAverage, expectedAverage);
            }
        }
    }
}
