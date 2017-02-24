using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.PL;
using System.Linq;

namespace T5.Brothership.PL.Test
{
    [TestClass]
    public class utUserTest
    {
        [TestMethod]
        public void LoadUserLoginTest()
        {
            brothershipEntities oDc = new brothershipEntities();

            //Use LINQ to select user login info
            var UserLogin = (from u in oDc.Users
                             join ul in oDc.UserLogins on u.ID equals ul.UserID
                             where u.UserName == "TestUserOne"
                            select new
                            {
                                u.UserName,
                                ul.Password
                            }).FirstOrDefault();

            //checks the password based on username
            Assert.AreEqual(UserLogin.Password, "Password");
        }

        [TestMethod]
        public void InsertUserLoginTest()
        {

            //This is how you insert user
            brothershipEntities context = new brothershipEntities();

            UserLogin expectedUser = new UserLogin { Password = "password" };
            context.UserLogins.Add(expectedUser);

            context.SaveChanges();

            expectedUser.User = new User
            {
                ID = expectedUser.UserID,
                Bio = "Hello",
                DateJoined = DateTime.Now,
                DOB = new DateTime(1990, 4, 1),
                Email = @"MrTestUser@gmail.com",
                NationalityID = 1,
                ProfileImagePath = @"MrTestUser.png",
                UserName = "MrTestUser",
                UserTypeID = 1,
                Gender = "m"
            };

            context.SaveChanges();

            var actualUser = context.UserLogins.FirstOrDefault(p => p.UserID == expectedUser.UserID);

            Assert.AreEqual(expectedUser.UserID, actualUser.UserID);
            Assert.AreEqual(expectedUser.User.ID, actualUser.User.ID);
            Assert.AreEqual(expectedUser.User.Nationality, actualUser.User.Nationality);
            Assert.AreEqual(expectedUser.User.Bio, actualUser.User.Bio);
            Assert.AreEqual(expectedUser.User.UserName, actualUser.User.UserName);
            Assert.AreEqual(expectedUser.User.ProfileImagePath, actualUser.User.ProfileImagePath);
        }

        [TestMethod]
        public void DeleteUserLoginTest()
        {
            brothershipEntities oDc = new brothershipEntities();

            //Use LINQ to select user login info
            var User = (from u in oDc.Users                             
                        where u.UserName == "UnitTestUser"
                        select u).FirstOrDefault();

            var Login = (from ul in oDc.UserLogins
                        where ul.Password == "UnitTestPassword"
                        select ul).FirstOrDefault();


            if (User != null && Login != null)
            {
                oDc.Users.Remove(User);
                oDc.UserLogins.Remove(Login);
                oDc.SaveChanges();

                var UserDeleted = (from u in oDc.Users
                            where u.UserName == "UnitTestUser"
                            select u).FirstOrDefault();

                var LoginDeleted = (from ul in oDc.UserLogins
                             where ul.Password=="UnitTestPassword"
                             select ul).FirstOrDefault();

                Assert.IsNull(UserDeleted);
                Assert.IsNull(LoginDeleted);
            }
            else
            {
                Assert.Fail("Record to delete not found");
            }
        }

        [TestMethod]
        public void InsertUserTest()
        {//this one has no referential integrity errors
            brothershipEntities oDc = new brothershipEntities();
            //Use LINQ to create a new user
            var UserStart = (from u in oDc.Users
                             select u).ToList();

            User newUser = new User();
            

            //newUser.ID = -1;            
            newUser.UserName = "UnitTestUser";           
            newUser.Email = "UnitTest@email";
            newUser.DateJoined = DateTime.Now;
            newUser.DOB = DateTime.Parse("01/01/1990");
            newUser.Bio = "UnitTestBio";
            newUser.ProfileImagePath = "UnitTestPath";
            newUser.Gender = "U";
            //Add nationality

            //Insert it all then test each individually
            oDc.Users.Add(newUser);            
            oDc.SaveChanges();

            var UserEnd = (from u in oDc.Users
                             select u).ToList();

            Assert.AreEqual(UserStart.Count + 1, UserEnd.Count);
        }
    }
}
