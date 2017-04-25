using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Exceptions;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ManagerFakes
{
    public class UserManagerFake : IUserManager
    {
        public List<User> _fakeUsers;
        public string ValidPassword { get; set; }
        public UserManagerFake()
        {
            _fakeUsers = CreateFakeUsers();
        }

        public Task Add(User user, string password)
        {
            return Task.Run(() => { _fakeUsers.Add(user); });
        }

        public void Dispose()
        {
        }

        public List<User> GetAllUsers()
        {
            return _fakeUsers;
        }

        public User GetById(int id)
        {
            return _fakeUsers.FirstOrDefault(p => p.ID == id);
        }

        public User GetByUserName(string userName)
        {
            return _fakeUsers.FirstOrDefault(p => p.UserName == userName);
        }

        public List<User> GetRandomFeaturedUsers(int randomCount, List<User> usersToExclude = null)
        {
            return _fakeUsers;
        }

        public List<User> GetRandomPopularUsers(int randomCount, int topUserCount, List<User> usersToExclude = null)
        {
            return _fakeUsers;
        }

        public List<User> GetSearchedUsers(string search)
        {
            return _fakeUsers;
        }

        public List<User> GetUsersByGame(int igdbid)
        {
            return _fakeUsers;
        }

        public User Login(string userNameOrEmail, string password)
        {
            var user = _fakeUsers.FirstOrDefault(p => p.UserName == userNameOrEmail);
            return user == null ? new InvalidUser() : user;
        }

        public Task Update(User updatedUser)
        {
            var user = _fakeUsers.First(p => p.ID == updatedUser.ID);
            int index = _fakeUsers.IndexOf(user);
            _fakeUsers[index] = updatedUser;
            return Task.Run(() => { });
        }

        public void UpdatePassword(string currentPassword, string newPassword, User user)
        {
            if (currentPassword != ValidPassword)
            {
                throw new InvalidPasswordException("Password is incorrect");
            }
        }

        public bool UserNameExists(string userName)
        {
            return _fakeUsers.Exists(p => p.UserName == userName);
        }

        private List<User> CreateFakeUsers()
        {
            var users = new List<User>();
            users.Add(new User
            {
                ID = 1,
                UserName = "TestUserOne",
                Email = "Testing123@yahoo.com",
                Bio = "This is my bio",
                ProfileImagePath = "../Images/TestUserOne/Pofile.png",
                DateJoined = new DateTime(2017, 2, 23),
                DOB = new DateTime(1988, 11, 12),
                GenderId = 1,
                UserType = new UserType { ID = 1, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 1,
                UserIntegrations = new List<UserIntegration>(),
                UserLogin = new UserLogin
                {
                    PasswordHash = "5Efg7nxAjJdkjIsZECyAWGA10mMixUnUiatbAgfcX3g=",
                    UserID = 1,
                    Salt = "b9qo1clGZ0q/99JkBJevOJGjU6JGUhmy"
                },

            });

            users.Add(new User
            {
                ID = 2,
                UserName = "TestUserTwo",
                Email = "TestingUser2@yahoo.com",
                Bio = "Hello I am the second test user",
                ProfileImagePath = "../Images/TestUserTwo/Profile.png",
                DateJoined = new DateTime(2017, 2, 22),
                DOB = new DateTime(1980, 1, 27),
                GenderId = 1,
                UserType = new UserType { ID = 2, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 1,
                UserIntegrations = new List<UserIntegration>(),
                UserLogin = new UserLogin
                {
                    PasswordHash = "qaNdZwpUFt18tcaJtAJBr4rTkwmy6uwvB1zlm4MLh7g=",
                    UserID = 2,
                    Salt = "QBKzLfLzbtRIS19vkbguqgPakJ+BKQre"
                }
            });

            users.Add(new User
            {
                ID = 3,
                UserName = "TestUserThree",
                Email = "TestingUser3@yahoo.com",
                Bio = "'Hello I am the Third test user",
                ProfileImagePath = "../Images/TestUserThree/Profile.png",
                DateJoined = new DateTime(2017, 1, 5),
                DOB = new DateTime(1990, 6, 1),
                GenderId = 1,
                UserType = new UserType { ID = 2, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 2,
                UserIntegrations = new List<UserIntegration>(),
                UserLogin = new UserLogin
                {
                    PasswordHash = "/HOXKid5g4YaNZNitnwyYnnoy7CecL6lxaDil4fjHmE=",
                    UserID = 3,
                    Salt = "xbiS4ItBbOzkl/9PfDJzvs8IYK6aiH6q",
                }
            });

            users.Add(new User
            {
                ID = 4,
                UserName = "TestUserFour",
                Email = "TestingUser4@yahoo.com",
                Bio = "Hello I am the Fourth test user",
                ProfileImagePath = "../Images/TestUserFour/Profile.png",
                DateJoined = new DateTime(2017, 1, 23),
                DOB = new DateTime(1991, 4, 27),
                GenderId = 1,
                UserType = new UserType { ID = 2, Description = "FeaturedUser" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 2,
                UserIntegrations = new List<UserIntegration>(),
                UserLogin = new UserLogin
                {
                    PasswordHash = "zAhNMBQ4/Ld4Qg19Sm3vukDyyu+rYnRAgIBw5t2wjTM=",
                    UserID = 4,
                    Salt = "yA2zLBkEQzjGT8wBUoNf6OcVt+J+/2gV"
                }
            });

            users.Add(new User
            {
                ID = 5,
                UserName = "TestUserFive",
                Email = "TestingUser5@yahoo.com",
                Bio = "Hello I am the Fifth test user",
                ProfileImagePath = "../Images/TestUserFive/Profile.png",
                DateJoined = new DateTime(2017, 1, 19),
                DOB = new DateTime(1962, 7, 9),
                GenderId = 1,
                UserType = new UserType { ID = 1, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 1,
                UserIntegrations = new List<UserIntegration>(),
                UserLogin = new UserLogin
                {
                    PasswordHash = "VCexSa7lVH7IvZ4qsABqRcnjWJLte24mPCaTK4DbHNY=",
                    UserID = 1,
                    Salt = "VuQePlIuVbuwkygTSwbHCjkTsqy5cLgB"
                }
            });

            users.Add(new User
            {
                ID = 5,
                UserName = "TestUserSix",
                Email = "TestingUser6@yahoo.com",
                Bio = "Hello I am the Sixth test user",
                ProfileImagePath = "../Images/TestUserSix/Profile.png",
                DateJoined = new DateTime(2017, 1, 14),
                DOB = new DateTime(1955, 5, 7),
                GenderId = 1,
                UserType = new UserType { ID = 2, Description = "FeaturedUser" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 2,
                UserRatings = new List<UserRating>(),
                UserIntegrations = new List<UserIntegration>(),
                UserLogin = new UserLogin
                {
                    PasswordHash = "VCexSa7lVH7IvZ4qsABqRcnjWJLte24mPCaTK4DbHNY=",
                    UserID = 1,
                    Salt = "VuQePlIuVbuwkygTSwbHCjkTsqy5cLgB"
                }
            });

            return users;
        }
    }
}
