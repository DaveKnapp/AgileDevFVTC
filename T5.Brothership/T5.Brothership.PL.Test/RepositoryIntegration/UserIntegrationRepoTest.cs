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
    /// <summary>
    /// Summary description for UserIntegrationRepoTest
    /// </summary>
    [TestClass]
    public class UserIntegrationRepoTest
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
        public void Add_ActualAddedData_EqualsExpectedData()
        {
            var expectedUserIntegration = new UserIntegration
            {
                UserID = 0x2,
                IntegrationTypeID = 0x1,
                Token = "asdlkfjsdlafjldasjf"
            };

            UserIntegration actualUserIntegration;

            using (var userIntegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                userIntegrationRepo.Add(expectedUserIntegration);
                userIntegrationRepo.SaveChanges();
                actualUserIntegration = userIntegrationRepo.GetById(expectedUserIntegration.UserID, expectedUserIntegration.IntegrationTypeID);
            }

            AssertUserInegrationsEqual(expectedUserIntegration, actualUserIntegration);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_actualDataNull()
        {
            UserIntegration actualUserIntegration;
            var userIntegrationToDelete = AddandGetTestUserIntegration();

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
            var userIntegrationToDelete = AddandGetTestUserIntegration();
            UserIntegration actualUserIntegration;

            using (var userIntegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                userIntegrationRepo.Delete(userIntegrationToDelete.UserID, userIntegrationToDelete.IntegrationTypeID);
                userIntegrationRepo.SaveChanges();
                actualUserIntegration = userIntegrationRepo.GetById(userIntegrationToDelete.UserID, userIntegrationToDelete.IntegrationTypeID);
            }

            Assert.IsNull(actualUserIntegration);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_Count_EqualActual()
        {
            const int expectedCount = 0x2;
            int actualCount;
            using (var userInegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                actualCount = userInegrationRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAllByUser_Count_EqualActual()
        {
            const int expectedUserId = 0x3;
            const int expectedCount = 0x1;
            int actualCount;
            using (var userInegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
            {
                actualCount = userInegrationRepo.GetAllByUser(expectedUserId).Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_EqualExpectedData()
        {
            var expectedUserInegration = new UserIntegration
            {
                UserID = 0x3,
                IntegrationTypeID = 0x1,
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
        public void Update_ActualUpdatedData_EqualsExpectedData()
        {
            var expectedUserIntegration = new UserIntegration
            {
                UserID = 0x3,
                IntegrationTypeID = 0x1,
                Token = "asdlkfjsdlafjldasjf"
            };
            UserIntegration actualUserIntegration;

            using (var userIntegrationRepo = new UserIntegrationRepository(new brothershipEntities()))
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
                UserID = 0x4,
                IntegrationTypeID = 0x1,
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
