using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    internal class UserFakeRepository : IUserRepository
    {
        private List<User> _fakeUsers = new List<User>();

        public UserFakeRepository()
        {
            InitializeUsers();
        }

        public void Add(User entity)
        {
            entity.UserLogin.UserID = GenerateUserId();
            _fakeUsers.Add(entity);
        }

        public void Delete(int id)
        {
            var user = _fakeUsers.Single(p => p.ID == id);
            _fakeUsers.Remove(user);

        }

        public void Delete(User entity)
        {
            _fakeUsers.Remove(entity);
        }

        public void Dispose()
        {
            _fakeUsers = null;
        }

        public IQueryable<User> GetAll()
        {
            return _fakeUsers.AsQueryable();
        }

        public User GetById(int id)
        {
            return _fakeUsers.FirstOrDefault(p => p.ID == id);
        }

        public User GetByUsernameOrEmail(string userNameOrEmail)
        {
            return _fakeUsers.FirstOrDefault(p => p.UserName == userNameOrEmail || p.Email == userNameOrEmail);
        }

        public void SaveChanges()
        {

        }

        public void Update(User entity)
        {
            var userIndex = _fakeUsers.IndexOf(entity);
            _fakeUsers[userIndex] = entity;
        }

        private int GenerateUserId()
        {
            return _fakeUsers.Max(p => p.ID);
        }

        private void InitializeUsers()
        {
            _fakeUsers.Add(new User
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
                UserLogin = new UserLogin
                {
                    PasswordHash = "5Efg7nxAjJdkjIsZECyAWGA10mMixUnUiatbAgfcX3g=",
                    UserID = 1,
                    Salt = "b9qo1clGZ0q/99JkBJevOJGjU6JGUhmy"
                },
                Games = CreateUserOneGames()

            });

            _fakeUsers.Add(new User
            {
                ID = 2,
                UserName = "TestUserTwo",
                Email = "TestingUser2@yahoo.com",
                Bio = "Hello I am the second test user",
                ProfileImagePath = "../Images/TestUserTwo/Profile.png",
                DateJoined = new DateTime(2017, 2, 22),
                DOB = new DateTime(1980, 1, 27),
                GenderId = 1,
                UserType = new UserType { ID = 1, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 1,
                UserLogin = new UserLogin
                {
                    PasswordHash = "qaNdZwpUFt18tcaJtAJBr4rTkwmy6uwvB1zlm4MLh7g=",
                    UserID = 2,
                    Salt = "QBKzLfLzbtRIS19vkbguqgPakJ+BKQre"
                }
            });

            _fakeUsers.Add(new User
            {
                ID = 3,
                UserName = "TestUserThree",
                Email = "TestingUser3@yahoo.com",
                Bio = "'Hello I am the Third test user",
                ProfileImagePath = "../Images/TestUserThree/Profile.png",
                DateJoined = new DateTime(2017, 1, 5),
                DOB = new DateTime(1990, 6, 1),
                GenderId = 1,
                UserType = new UserType { ID = 1, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 1,
                UserLogin = new UserLogin
                {
                    PasswordHash = "/HOXKid5g4YaNZNitnwyYnnoy7CecL6lxaDil4fjHmE=",
                    UserID = 3,
                    Salt = "xbiS4ItBbOzkl/9PfDJzvs8IYK6aiH6q",
                }
            });

            _fakeUsers.Add(new User
            {
                ID = 4,
                UserName = "TestUserFour",
                Email = "TestingUser4@yahoo.com",
                Bio = "Hello I am the Fourth test user",
                ProfileImagePath = "../Images/TestUserFour/Profile.png",
                DateJoined = new DateTime(2017, 1, 23),
                DOB = new DateTime(1991, 4, 27),
                GenderId = 1,
                UserType = new UserType { ID = 1, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 1,
                UserLogin = new UserLogin
                {
                    PasswordHash = "zAhNMBQ4/Ld4Qg19Sm3vukDyyu+rYnRAgIBw5t2wjTM=",
                    UserID = 4,
                    Salt = "yA2zLBkEQzjGT8wBUoNf6OcVt+J+/2gV"
                }
            });

            _fakeUsers.Add(new User
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
                UserLogin = new UserLogin
                {
                    PasswordHash = "VCexSa7lVH7IvZ4qsABqRcnjWJLte24mPCaTK4DbHNY=",
                    UserID = 1,
                    Salt = "VuQePlIuVbuwkygTSwbHCjkTsqy5cLgB"
                }
            });
        }

        private ICollection<Game> CreateUserOneGames()
        {
            List<Game> games = new List<Game>();

            games.Add(new Game
            {
                ID = 2,
                Title = "Batlefield 1",
                igdbID = 4325,
                CategoryID = 4
            });

            games.Add(new Game
            {
                ID = 3,
                Title = "Civilization V",
                igdbID = 523,
                CategoryID = 6
            });

            games.Add(new Game
            {
                ID = 4,
                Title = "Resident Evil 7",
                igdbID = 324,
                CategoryID = 5
            });

            return games;
        }
    }
}
