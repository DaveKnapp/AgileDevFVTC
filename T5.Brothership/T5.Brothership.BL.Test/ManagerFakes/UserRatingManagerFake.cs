using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.BL.Managers;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ManagerFakes
{
    public class UserRatingManagerFake : IUserRatingManager
    {
        private List<UserRating> _fakeUserRatings;

        public UserRatingManagerFake()
        {
            _fakeUserRatings = CreateFakeUserRatings();
        }

        public void Add(UserRating userRating)
        {
            _fakeUserRatings.Add(userRating);
        }

        public void Dispose()
        {
        }

        public List<UserRating> GetAll()
        {
            return _fakeUserRatings.ToList();
        }

        public List<UserRating> GetAllByUserUserRatings(int ratedUserId)
        {
            return _fakeUserRatings.Where(p => p.UserBeingRatedID == ratedUserId).ToList();
        }

        public double GetAverageRating(int ratedUserId)
        {
            return _fakeUserRatings.Where(p => p.UserBeingRatedID == ratedUserId).Average(p => p.RatingID);
        }

        public UserRating GetById(int raterId, int userBeingRatedId)
        {
            return _fakeUserRatings.FirstOrDefault(p => p.RaterUserID == raterId && p.UserBeingRatedID == userBeingRatedId);
        }

        private List<UserRating> CreateFakeUserRatings()
        {
            return new List<UserRating>
            {
                new UserRating
                {
                    RaterUserID = 1,
                    UserBeingRatedID = 2,
                    Comment = "10/10 wouldWatchAgain",
                    RatingID = 5
                },

                new UserRating
                {
                    RaterUserID = 3,
                    UserBeingRatedID = 2,
                    Comment = "Okay",
                    RatingID = 4
                },

                new UserRating
                {
                    RaterUserID = 4,
                    UserBeingRatedID = 2,
                    Comment = "elbows too pointy",
                    RatingID = 1
                },

                new UserRating
                {
                    RaterUserID = 2,
                    UserBeingRatedID = 1,
                    Comment = "Alright",
                    RatingID = 3
                },

                new UserRating
                {
                    RaterUserID = 3,
                    UserBeingRatedID = 4,
                    Comment = "Fun Times",
                    RatingID = 4
                },

                new UserRating
                {
                    RaterUserID = 4,
                    UserBeingRatedID = 5,
                    Comment = "5/5 would watch again",
                    RatingID = 5
                },

                new UserRating
                {
                    RaterUserID = 5,
                    UserBeingRatedID = 3,
                    Comment = "Best stream ever",
                    RatingID = 5
                },

                new UserRating
                {
                    RaterUserID = 3,
                    UserBeingRatedID = 1,
                    Comment = "boring",
                    RatingID = 1
                }
            };
        }
    }
}
