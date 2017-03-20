using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;
using System.Linq;
using System.IO;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{
    [TestClass]
    public class UserIntegrationRepoTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var dbContext =  new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME))
            {
                SqlScriptRunner.RunAddTestDataScript(dbContext);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Add_ActualAddedData_ActualEqualsExpectedData()
        {
            var expectedUserIntegration = new UserIntegration
            {
                UserID = 2,
                IntegrationTypeID = 1,
                Token = "asdlkfjsdlafjldasjf"
            };

            UserIntegration actualUserIntegration;

            using (var userIntegrationRepo = new UserIntegrationRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                userIntegrationRepo.Add(expectedUserIntegration);
                userIntegrationRepo.SaveChanges();
                actualUserIntegration = userIntegrationRepo.GetById(expectedUserIntegration.UserID, expectedUserIntegration.IntegrationTypeID);
            }

            AssertUserInegrationsEqual(expectedUserIntegration, actualUserIntegration);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_ActualDataIsNull()
        {
            UserIntegration actualUserIntegration;
            var userIntegrationToDelete = AddandGetTestUserIntegration();

            using (var userIntegrationRepo = new UserIntegrationRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                userIntegrationRepo.Delete(userIntegrationToDelete);
                userIntegrationRepo.SaveChanges();
                actualUserIntegration = userIntegrationRepo.GetById(userIntegrationToDelete.UserID, userIntegrationToDelete.IntegrationTypeID);
            }

            Assert.IsNull(actualUserIntegration);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_ActualDataIsNull()
        {
            var userIntegrationToDelete = AddandGetTestUserIntegration();
            UserIntegration actualUserIntegration;

            using (var userIntegrationRepo = new UserIntegrationRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                userIntegrationRepo.Delete(userIntegrationToDelete.UserID, userIntegrationToDelete.IntegrationTypeID);
                userIntegrationRepo.SaveChanges();
                actualUserIntegration = userIntegrationRepo.GetById(userIntegrationToDelete.UserID, userIntegrationToDelete.IntegrationTypeID);
            }

            Assert.IsNull(actualUserIntegration);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_AllUserIntegrationsReturned_CountEqualsActual()
        {
            const int expectedCount = 2;
            int actualCount;
            using (var userInegrationRepo = new UserIntegrationRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualCount = userInegrationRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAllByUser_AllUserIntegrationsReturned_CountEqualsActual()
        {
            const int expectedUserId = 3;
            const int expectedCount = 1;
            int actualCount;
            using (var userInegrationRepo = new UserIntegrationRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualCount = userInegrationRepo.GetAllByUser(expectedUserId).Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_ActualEqualsExpectedData()
        {
            var expectedUserInegration = new UserIntegration
            {
                UserID = 3,
                IntegrationTypeID = 1,
                Token = "lkjlkjlk;jlkjlk3jlkjlkj"
            };
            UserIntegration actualUserIntegration;

            using (var userInegrationRepo = new UserIntegrationRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualUserIntegration = userInegrationRepo.GetById(expectedUserInegration.UserID, expectedUserInegration.IntegrationTypeID);
            }

            AssertUserInegrationsEqual(expectedUserInegration, actualUserIntegration);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_WasUserIntegrationUpdated_ActualEqualsExpectedData()
        {
            var expectedUserIntegration = new UserIntegration
            {
                UserID = 3,
                IntegrationTypeID = 1,
                Token = "asdlkfjsdlafjldasjf"
            };
            UserIntegration actualUserIntegration;

            using (var userIntegrationRepo = new UserIntegrationRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                userIntegrationRepo.Update(expectedUserIntegration);
                userIntegrationRepo.SaveChanges();
                actualUserIntegration = userIntegrationRepo.GetById(expectedUserIntegration.UserID, expectedUserIntegration.IntegrationTypeID);
            }

            AssertUserInegrationsEqual(expectedUserIntegration, actualUserIntegration);
        }

        private UserIntegration AddandGetTestUserIntegration()
        {
            var userIntegration = new UserIntegration
            {
                UserID = 4,
                IntegrationTypeID = 1,
                Token = "asdfwewrewreaw34"
            };

            using (var userIntegrationRepo = new UserIntegrationRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
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
