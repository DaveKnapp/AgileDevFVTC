using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.PL.Repositories;
using System.Linq;
using T5.Brothership.Entities.Models;
using System.IO;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{
    [TestClass]
    public class UserRatingRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            var script = File.ReadAllText(FilePaths.ADD_TEST_DATA_SCRIPT_PATH);

            using (brothershipEntities dataContext = new brothershipEntities())
            {
                dataContext.Database.ExecuteSqlCommand(script);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Add_WasRatingAdded_ActualEqualsExpectedData()
        {
            var expectedUserRating = new UserRating
            {
                RaterUserID = 4,
                UserBeingRatedID = 3,
                Comment = "Test comment",
                RatingID = 5
            };

            UserRating actualUserRating;

            using (var userRatingRepo = new UserRatingRepository(new brothershipEntities()))
            {
                userRatingRepo.Add(expectedUserRating);
                userRatingRepo.SaveChanges();
                actualUserRating = userRatingRepo.GetById(expectedUserRating.RaterUserID, expectedUserRating.UserBeingRatedID);
            }

            AssertUserRatingsEqual(expectedUserRating, actualUserRating);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_ActualDataIsNull()
        {
            UserRating actualUserRating;
            var userRatingToDelete = AddandGetTestUserRating();

            using (var userRatingRepo = new UserRatingRepository(new brothershipEntities()))
            {
                userRatingRepo.Delete(userRatingToDelete);
                userRatingRepo.SaveChanges();
                actualUserRating = userRatingRepo.GetById(userRatingToDelete.RaterUserID, userRatingToDelete.UserBeingRatedID);
            }

            Assert.IsNull(actualUserRating);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_ActualDataIsNull()
        {
            var userIntegrationToDelete = AddandGetTestUserRating();
            UserRating actualUserRating;

            using (var userRatingRepo = new UserRatingRepository(new brothershipEntities()))
            {
                userRatingRepo.Delete(userIntegrationToDelete.RaterUserID, userIntegrationToDelete.UserBeingRatedID);
                userRatingRepo.SaveChanges();
                actualUserRating = userRatingRepo.GetById(userIntegrationToDelete.RaterUserID, userIntegrationToDelete.UserBeingRatedID);
            }

            Assert.IsNull(actualUserRating);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_AllUserRatingsReturned_CountEqualsActual()
        {
            const int expectedCount = 8;
            int actualCount;
            using (var userRatingRepoRepo = new UserRatingRepository(new brothershipEntities()))
            {
                actualCount = userRatingRepoRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAllByUser_AllUserRatingsReturned_CountEqualsActual()
        {
            const int expectedUserId = 2;
            const int expectedCount = 3;
            int actualCount;
            using (var userRatingRepo = new UserRatingRepository(new brothershipEntities()))
            {
                actualCount = userRatingRepo.GetAllByUserId(expectedUserId).Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_ActualEqualsExpectedData()
        {
            var expectedUserRating = new UserRating
            {
                RaterUserID = 2,
                UserBeingRatedID = 1,
                Comment = "Alright",
                RatingID = 3
            };
            UserRating actualUserRating;

            using (var userRatingRepo = new UserRatingRepository(new brothershipEntities()))
            {
                actualUserRating = userRatingRepo.GetById(expectedUserRating.RaterUserID, expectedUserRating.UserBeingRatedID);
            }

            AssertUserRatingsEqual(expectedUserRating, actualUserRating);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_WasRatingUpdated_ActualEqualsExpectedData()
        {
            var expectedUserRating = new UserRating
            {
                RaterUserID = 2,
                UserBeingRatedID = 1,
                Comment = "Winning",
                RatingID = 3
            };
            UserRating actualUserRating;

            using (var userRatingRepo = new UserRatingRepository(new brothershipEntities()))
            {
                userRatingRepo.Update(expectedUserRating);
                userRatingRepo.SaveChanges();
                actualUserRating = userRatingRepo.GetById(expectedUserRating.RaterUserID, expectedUserRating.UserBeingRatedID);
            }

            AssertUserRatingsEqual(expectedUserRating, actualUserRating);
        }

        private UserRating AddandGetTestUserRating()
        {
            var userRating = new UserRating
            {
                RaterUserID = 4,
                UserBeingRatedID = 3,
                Comment = "Test comment",
                RatingID = 5
            };

            using (var userRatingRepo = new UserRatingRepository(new brothershipEntities()))
            {
                userRatingRepo.Add(userRating);
                userRatingRepo.SaveChanges();
            }

            return userRating;
        }

        private void AssertUserRatingsEqual(UserRating expectedUserRating, UserRating actualUserRating)
        {
            Assert.AreEqual(expectedUserRating.RaterUserID, actualUserRating.RaterUserID);
            Assert.AreEqual(expectedUserRating.UserBeingRatedID, actualUserRating.UserBeingRatedID);
            Assert.AreEqual(expectedUserRating.Comment, actualUserRating.Comment);
            Assert.AreEqual(expectedUserRating.RatingID, actualUserRating.RatingID);
        }
    }

}
