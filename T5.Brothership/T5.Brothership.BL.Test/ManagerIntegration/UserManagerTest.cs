using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using T5.Brothership.PL;
using T5.Brothership.PL.Test;
using T5.Brothership.Entities.Models;
using T5.Brothership.BL.Managers;
using System.Threading.Tasks;
using System.Linq;

namespace T5.Brothership.BL.Test.ManagerIntegration
{
    [TestClass]
    public class UserManagerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var dbContext =  DataContextCreator.CreateTestContext())
            {
                SqlScriptRunner.RunAddTestDataScript(dbContext);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public async Task Add_WasDataAdded_ExpectedDataEqualsActualData()
        {
            const string expectedUserPassword = "TestPassword";
            User expectedUser = new User
            {
                UserName = "SuperFly",
                Bio = "Hello This is me",
                DOB = new DateTime(1990, 3, 12),
                Email = "MrSuper@gmail.com",
                GenderId = 1,
                NationalityID = 1,
                UserTypeID = 1,
                ProfileImagePath = "MrFlyImagePath.jpg"
            };

            User actualUser;
            using (UserManager userManger = new UserManager(new BrothershipUnitOfWork(DataContextCreator.CreateTestContext())))
            {
                await userManger.Add(expectedUser, expectedUserPassword);
                actualUser = userManger.GetById(expectedUser.ID);
            }

            Assert.IsNotNull(actualUser);
            Assert.AreNotEqual(actualUser.ID, 0);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public async Task GetById_WasCorrentDataReturned_ExpectedDataEqualsActualData()
        {
            var expectedUser = new User
            {
                ID = 1,
                UserName = "TestUserOne",
                Email = "Testing123@yahoo.com",
                Bio = "This is my bio",
                ProfileImagePath = "../Images/TestUserOne/profile.png",
                DateJoined = new DateTime(2017, 2, 20),
                DOB = new DateTime(1988, 11, 12),
                GenderId = 1,
                UserTypeID = 1,
                NationalityID = 1
            };

            User actualUser;
            using (UserManager userManger = new UserManager(new BrothershipUnitOfWork(DataContextCreator.CreateTestContext())))
            {
                userManger.GetById(expectedUser.ID);
                actualUser = userManger.GetById(expectedUser.ID);
                AssertUsersEqual(expectedUser, actualUser);

                foreach (var game in expectedUser.Games)
                {
                    Assert.IsNotNull(actualUser.Games.FirstOrDefault(p => p.igdbID == game.igdbID));
                }
            }
        }

        [TestCategory("IntegrationTest"), TestMethod]
        public async Task Update_WasDataUpdated_ExpectedDataEqualsActual()
        {
            using (var userManager = new UserManager(new BrothershipUnitOfWork(DataContextCreator.CreateTestContext())))
            {
                const int expectedCount = 4;
                const int userId = 1;

                var expectedUser = new User
                {
                    ID = 1,
                    Bio = "UpdatedBio",
                    DOB = new DateTime(1999, 3, 22),
                    Email = "UserOneUpdatedEmail@gmail.com",
                    GenderId = 2,
                    DateJoined = new DateTime(2017, 2, 20),
                    ProfileImagePath = "UpdatedImagePath.jpg",
                    UserTypeID = 1,
                    UserName = "TestUserOne",
                    NationalityID = 1,

                    Games = new List<Game>{ new Game {igdbID = 4325},
                                                     new Game {igdbID = 1277},
                                                     new Game {igdbID = 2276},
                                                     new Game {igdbID = 1039}}
                };

                await userManager.Update(expectedUser);

                var actualUser = userManager.GetById(userId);

                AssertUsersEqual(expectedUser, actualUser);

                Assert.AreEqual(expectedCount, actualUser.Games.Count);

                foreach (var game in expectedUser.Games)
                {
                    Assert.IsNotNull(actualUser.Games.FirstOrDefault(p => p.igdbID == game.igdbID));
                }
            }
        }
        private void AssertUsersEqual(User expected, User actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Bio, actual.Bio);
            Assert.AreEqual(expected.DateJoined, actual.DateJoined);
            Assert.AreEqual(expected.DOB, actual.DOB);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.GenderId, actual.GenderId);
            Assert.AreEqual(expected.NationalityID, actual.NationalityID);
            Assert.AreEqual(expected.ProfileImagePath, actual.ProfileImagePath);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.UserTypeID, actual.UserTypeID);
        }
    }
}
