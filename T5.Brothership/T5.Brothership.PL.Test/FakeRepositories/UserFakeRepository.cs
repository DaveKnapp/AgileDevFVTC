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

        public IQueryable<User> GetFeaturedUsers()
        {
            return _fakeUsers.Where(p => p.UserTypeID == (int)UserType.UserTypes.FeaturedUser).AsQueryable();
        }

        public IQueryable<User> GetMostPopularUsers(int count)
        {
            var users = _fakeUsers.Where(p => p.UserRatings.Count > 0);

            return users.OrderByDescending(p => p.UserRatings.Average(r => r.RatingID) * p.UserRatings.Count).AsQueryable();
        }

        public IQueryable<User> GetTopRatedUsers(int count)
        {
            return _fakeUsers.OrderByDescending(p => p.UserRatings.Average(i => i.RatingID)).Take(count).AsQueryable();
        }

        public void SaveChanges()
        {

        }

        public void Update(User entity)
        {
            var user = _fakeUsers.FirstOrDefault(p => p.ID == entity.ID);
            var index = _fakeUsers.IndexOf(user);
            _fakeUsers[index] = entity;
        }

        private ICollection<UserRating> CreateUserFiveRatings()
        {
            return new List<UserRating>
            {
               new UserRating
               {
                   UserBeingRatedID = 5,
                   RaterUserID =7,
                   Comment = "Great",
                   RatingID = 4
               },

                new UserRating
               {
                   UserBeingRatedID = 5,
                   RaterUserID =6,
                   Comment = "okay",
                   RatingID = 5
               },
                new UserRating
               {
                   UserBeingRatedID = 5,
                   RaterUserID =1,
                   Comment = "Fail",
                   RatingID = 1
               },
                  new UserRating
               {
                   UserBeingRatedID = 5,
                   RaterUserID =4,
                   Comment = "good",
                   RatingID = 4
               },
                    new UserRating
               {
                   UserBeingRatedID = 5,
                   RaterUserID =2,
                   Comment = "best",
                   RatingID = 5
               },
                      new UserRating
               {
                   UserBeingRatedID = 5,
                   RaterUserID =10,
                   Comment = "perfext",
                   RatingID = 5
               }
            };
        }

        private ICollection<UserRating> CreateUserFourRatings()
        {
            return new List<UserRating>
            {
               new UserRating
               {
                   UserBeingRatedID = 4,
                   RaterUserID =1,
                   Comment = "Best",
                   RatingID = 5
               },

                new UserRating
               {
                   UserBeingRatedID = 4,
                   RaterUserID =2,
                   Comment = "okay",
                   RatingID = 5
               }
            };
        }

        private ICollection<Game> CreateUserOneGames()
        {
            List<Game> games = new List<Game>();

            games.Add(new Game
            {
                ID = 2,
                Title = "Batlefield 1",
                igdbID = 4325,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 5 } }
            });

            games.Add(new Game
            {
                ID = 3,
                Title = "Civilization V",
                igdbID = 523,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 11 } }
            });

            games.Add(new Game
            {
                ID = 4,
                Title = "Resident Evil 7",
                igdbID = 324,
                GameCategories = new List<GameCategory> { new GameCategory { ID = 31 } }
            });

            return games;
        }

        private ICollection<UserRating> CreateUserOneRatings()
        {
            return new List<UserRating>
            {
               new UserRating
               {
                   UserBeingRatedID = 1,
                   RaterUserID =2,
                   Comment = "Great",
                   RatingID = 4
               },

                new UserRating
               {
                   UserBeingRatedID = 1,
                   RaterUserID =5,
                   Comment = "okay",
                   RatingID = 3
               },
                new UserRating
               {
                   UserBeingRatedID = 1,
                   RaterUserID =4,
                   Comment = "Fail",
                   RatingID = 1
               }
            };
        }

        private ICollection<UserRating> CreateUserThreeRatings()
        {
            return new List<UserRating>
            {
               new UserRating
               {
                   UserBeingRatedID = 3,
                   RaterUserID =7,
                   Comment = "Great",
                   RatingID = 4
               },

                new UserRating
               {
                   UserBeingRatedID = 3,
                   RaterUserID =6,
                   Comment = "okay",
                   RatingID = 2
               },
                new UserRating
               {
                   UserBeingRatedID = 3,
                   RaterUserID =5,
                   Comment = "Fail",
                   RatingID = 1
               },
                  new UserRating
               {
                   UserBeingRatedID = 3,
                   RaterUserID =4,
                   Comment = "Fail",
                   RatingID = 1
               },
                    new UserRating
               {
                   UserBeingRatedID = 3,
                   RaterUserID =2,
                   Comment = "Fail",
                   RatingID = 1
               },
                      new UserRating
               {
                   UserBeingRatedID = 3,
                   RaterUserID =1,
                   Comment = "Fail",
                   RatingID = 1
               }
            };
        }

        private ICollection<UserRating> CreateUserTwoRatings()
        {
            return new List<UserRating>
            {
               new UserRating
               {
                   UserBeingRatedID = 2,
                   RaterUserID =1,
                   Comment = "Great",
                   RatingID = 5
               },

                new UserRating
               {
                   UserBeingRatedID = 2,
                   RaterUserID =5,
                   Comment = "okay",
                   RatingID = 4
               },
                new UserRating
               {
                   UserBeingRatedID = 2,
                   RaterUserID =4,
                   Comment = "Fail",
                   RatingID = 2
               }
            };
        }

        private int GenerateUserId()
        {
            return _fakeUsers.Max(p => p.ID) + 1;
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
                UserRatings = CreateUserOneRatings(),
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
                UserType = new UserType { ID = 2, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 1,
                UserLogin = new UserLogin
                {
                    PasswordHash = "qaNdZwpUFt18tcaJtAJBr4rTkwmy6uwvB1zlm4MLh7g=",
                    UserID = 2,
                    Salt = "QBKzLfLzbtRIS19vkbguqgPakJ+BKQre"
                },
                UserRatings = CreateUserTwoRatings()
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
                UserType = new UserType { ID = 2, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 2,
                UserRatings = CreateUserThreeRatings(),
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
                UserType = new UserType { ID = 2, Description = "FeaturedUser" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 2,
                UserRatings = CreateUserThreeRatings(),
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
                UserRatings = CreateUserFiveRatings(),
                UserLogin = new UserLogin
                {
                    PasswordHash = "VCexSa7lVH7IvZ4qsABqRcnjWJLte24mPCaTK4DbHNY=",
                    UserID = 1,
                    Salt = "VuQePlIuVbuwkygTSwbHCjkTsqy5cLgB"
                }
            });

            _fakeUsers.Add(new User
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
                UserLogin = new UserLogin
                {
                    PasswordHash = "VCexSa7lVH7IvZ4qsABqRcnjWJLte24mPCaTK4DbHNY=",
                    UserID = 1,
                    Salt = "VuQePlIuVbuwkygTSwbHCjkTsqy5cLgB"
                }
            });
        }
    }
}
