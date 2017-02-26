using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Dts.ManagedConnections;


namespace T5.Brothership.PL.Test.RepositoryIntigration
{
    /// <summary>
    /// Summary description for UserRepositoryTest
    /// </summary>
    [TestClass]
    public class UserRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            //TODO Refactor
            string script = File.ReadAllText(@"../../RepositoryIntigration/AddTestData.sql");

            using (brothershipEntities dataContext = new brothershipEntities())
            {
                dataContext.Database.ExecuteSqlCommand(script);
            }

        }
        [TestMethod]
        public void Insert_WasRecordInserted_ActualUserNotNull()
        {
            User expectedUser = new User
            {
                Bio = "Hello This is a test",
                DateJoined = DateTime.Now,
                DOB = new DateTime(1990, 3, 2),
                Email = "expectedUser@gmail.com",
                NationalityID = 1,
                Gender = "M",
                UserLogin = new UserLogin { Password = "PasswordTest" },
                UserName = "TEstUserName",
                ProfileImagePath = "TestUserImage.png",
                UserTypeID = 1
            };

            User actualUser = new User();
            using (UserRepository userRepo = new UserRepository())
            {
                userRepo.Insert(expectedUser);
                userRepo.Save();
                actualUser = userRepo.GetByID(expectedUser);
            }

            Assert.AreEqual(expectedUser.UserName, actualUser.UserName);
        }

        [TestMethod]
        public void GetByID_WasDataGot_ActualUserNotNull()
        {
            User expectedUser = new User
            {
                ID = 1,
                UserName = "TestUserOne",
                Email = "Testing123@yahoo.com",
                Bio = "This is my bio",
                ProfileImagePath = "../Images/TestUserOne/profile.png",
                DateJoined = new DateTime(2017,2,20),
                DOB = new DateTime(1988,11,12),
                Gender = "M",
                UserTypeID = 1,
                NationalityID = 1
            };

            User actualUser = new User();
            using (UserRepository userRepo = new UserRepository())
            {
                actualUser = userRepo.GetByID(expectedUser);
            }

            Assert.AreEqual(expectedUser.ID, actualUser.ID);
        }

        private void AssertUsersEqual(User expected, User actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Bio, actual.Bio);
            Assert.AreEqual(expected.DateJoined, actual.DateJoined);
            Assert.AreEqual(expected.DOB, actual.DOB);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Gender, actual.Gender);
            Assert.AreEqual(expected.NationalityID, actual.NationalityID);
            Assert.AreEqual(expected.ProfileImagePath, actual.ProfileImagePath);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.UserTypeID, actual.UserTypeID);
        }
    }
}
