using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.RepositoryIntigration
{
    /// <summary>
    /// Summary description for UserRepositoryTest
    /// </summary>
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void Insert_WasRecordInserted_DataExitstInDB()
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
            using (UserRepository userRepository = new UserRepository())
            {
                userRepository.Insert(expectedUser);
                userRepository.Save();
                actualUser = userRepository.GetByID(expectedUser);
            }

            Assert.AreEqual(expectedUser.UserName, actualUser.UserName);

        }
    }
}
