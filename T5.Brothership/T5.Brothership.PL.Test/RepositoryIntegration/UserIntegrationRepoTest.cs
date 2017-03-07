using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;
using System.Linq;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{
    /// <summary>
    /// Summary description for UserIntegrationRepoTest
    /// </summary>
    [TestClass]
    public class UserIntegrationRepoTest
    {

        
        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_Count_EqualActual()
        {
            int expectedCount = 2;
            int actualCount;
            using (var userInegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                actualCount = userInegrationRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_EqualExpectedData()
        {
            UserIntegration expectedUserInegration = new UserIntegration
            {
                UserID = 3,
                IntegrationTypeID = 1,
                Token = "lkjlkjlk;jlkjlk3jlkjlkj"
            };
            UserIntegration actualRating;

            using (var userInegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                actualRating = userInegrationRepo.GetById(expectedUserInegration.UserID, expectedUserInegration.IntegrationTypeID);
            }

            AssertUserInegrationsEqual(expectedUserInegration, actualRating);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAllByUser_Count_EqualActual()
        {
            int expectedUserId = 3;
            int expectedCount = 1;
            int actualCount;
            using (var userInegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                actualCount = userInegrationRepo.GetAllByUser(expectedUserId).Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Add_ActualAddedData_EqualsExpectedData()
        {
            UserIntegration expectedUserIntegration = new UserIntegration
            {
                UserID = 2,
                IntegrationTypeID = 1,
                Token = "asdlkfjsdlafjldasjf"
            };

            UserIntegration actualUserIntegration;

            using (var userIntegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                userIntegrationRepo.Add(expectedUserIntegration);
                userIntegrationRepo.SaveChanges();
                actualUserIntegration = userIntegrationRepo.GetById(expectedUserIntegration.UserID,expectedUserIntegration.IntegrationTypeID);
            }

            AssertUserInegrationsEqual(expectedUserIntegration, actualUserIntegration);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_ActualUpdatedData_EqualsExpectedData()
        {
            UserIntegration expectedUserIntegration = new UserIntegration
            {
                UserID = 2,
                IntegrationTypeID = 1,
                Token = "asdlkfjsdlafjldasjf"
            };
            UserIntegration actualUserIntegration;

            using (var userIntegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                userIntegrationRepo.Update(expectedUserIntegration);
                actualUserIntegration = userIntegrationRepo.GetById(expectedUserIntegration.UserID, expectedUserIntegration.IntegrationTypeID);
            }

            AssertUserInegrationsEqual(expectedUserIntegration, actualUserIntegration);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_actualDataNull()
        {
            UserIntegration actualUserIntegration;
            UserIntegration userIntegrationToDelete = AddandGetTestUserIntegration();

            using (var userIntegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                userIntegrationRepo.Delete(userIntegrationToDelete);
                userIntegrationRepo.SaveChanges();
                actualUserIntegration = userIntegrationRepo.GetById(userIntegrationToDelete.UserID, userIntegrationToDelete.IntegrationTypeID);
            }

            Assert.IsNull(actualUserIntegration);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_actualDataNull()
        {
            UserIntegration userIntegrationToDelete = AddandGetTestUserIntegration();
            UserIntegration actualUserIntegration;

            using (var userIntegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                userIntegrationRepo.Delete(userIntegrationToDelete.UserID, userIntegrationToDelete.IntegrationTypeID);
                userIntegrationRepo.SaveChanges();
                actualUserIntegration = userIntegrationRepo.GetById(userIntegrationToDelete.UserID, userIntegrationToDelete.IntegrationTypeID);
            }

            Assert.IsNull(actualUserIntegration);
        }

        private UserIntegration AddandGetTestUserIntegration()
        {
            var userIntegration = new UserIntegration
            {
                UserID = 4,
                IntegrationTypeID = 1,
                Token = "asdfwewrewreaw34"
            };

            using (var userIntegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                userIntegrationRepo.Add(userIntegration);
                userIntegrationRepo.SaveChanges();
            }

            return userIntegration;
        }

        private void AssertUserInegrationsEqual(UserIntegration expectedUserInegration, UserIntegration actualUserInegration)
        {
            Assert.AreEqual(expectedUserInegration.UserID, actualUserInegration.UserID);
            Assert.AreEqual(expectedUserInegration.Token, actualUserInegration.Token);
            Assert.AreEqual(expectedUserInegration.IntegrationTypeID, actualUserInegration.IntegrationTypeID);
        }
    }
}
