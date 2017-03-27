using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Managers;
using T5.Brothership.PL.Test;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ManagerUnitTests
{
    [TestClass]
    public class UserRatingManagerUnitTest
    {
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

        [TestMethod, TestCategory("UnitTest")]
        public void GetAllByUserId_AllRatingsReturned_expectedCountEqualsActual()
        {
            const int expectedCount = 3;
            const int userId = 2;
            using (var ratingManager = new UserRatingManager(new FakeBrothershipUnitOfWork()))
            {
                int actualCount = ratingManager.GetAllByUserUserRatings(userId).Count;
                Assert.AreEqual(expectedCount, actualCount);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public void GetById_CorrectRatingReturned_expectedRatingEqualsActual()
        {
            var expectedRating = new UserRating
            {
                RaterUserID = 3,
                UserBeingRatedID = 2,
                Comment = "Okay",
                RatingID = 4
            };

            using (var ratingManager = new UserRatingManager(new FakeBrothershipUnitOfWork()))
            {
                var actualRating = ratingManager.GetById(expectedRating.RaterUserID, expectedRating.UserBeingRatedID);
                AssertRatingsEqual(expectedRating, actualRating);
            }
        }

        private void AssertRatingsEqual(UserRating expected, UserRating actual)
        {
            Assert.AreEqual(expected.RaterUserID, actual.RaterUserID);
            Assert.AreEqual(expected.UserBeingRatedID, actual.UserBeingRatedID);
            Assert.AreEqual(expected.Comment, actual.Comment);
            Assert.AreEqual(expected.RatingID, actual.RatingID);
        }
    }
}
