using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    internal class UserRatingFakeRepository : IUserRatingRepository
    {
        private List<UserRating> _fakeUserRatings = new List<UserRating>();

        public UserRatingFakeRepository()
        {
            IntializeUserRatings();
        }

        public void Add(UserRating entity)
        {
            _fakeUserRatings.Add(entity);
        }

        public void Delete(UserRating entity)
        {
            var userRating = _fakeUserRatings.FirstOrDefault(p => p.RaterUserID == entity.RaterUserID &&
                                                                       p.UserBeingRatedID == entity.UserBeingRatedID);
            _fakeUserRatings.Remove(userRating);
        }

        public void Delete(int raterId, int userBeingRatedId)
        {
            var userRating = _fakeUserRatings.FirstOrDefault(p => p.RaterUserID == raterId &&
                                                              p.UserBeingRatedID == userBeingRatedId);
            _fakeUserRatings.Remove(userRating);
        }

        public IQueryable<UserRating> GetAll()
        {
            return _fakeUserRatings.AsQueryable();
        }

        public IQueryable<UserRating> GetAllByUserId(int ratedUserId)
        {
            return _fakeUserRatings.Where(p => p.UserBeingRatedID == ratedUserId).AsQueryable();
        }

        public UserRating GetById(int raterId, int userBeingRatedId)
        {
            return _fakeUserRatings.FirstOrDefault(p => p.UserBeingRatedID == userBeingRatedId &&
                                                      p.RaterUserID == raterId);
        }

        public void SaveChanges()
        {
        }

        public void Update(UserRating entity)
        {
            var entityIndex = _fakeUserRatings.IndexOf(entity);
            _fakeUserRatings[entityIndex] = entity;
        }

        private void IntializeUserRatings()
        {
            _fakeUserRatings.Add(new UserRating
            {
                RaterUserID = 1,
                UserBeingRatedID = 2,
                Comment = "10/10 wouldWatchAgain",
                RatingID = 5
            });

            _fakeUserRatings.Add(new UserRating
            {
                RaterUserID = 3,
                UserBeingRatedID = 2,
                Comment = "Okay",
                RatingID = 4
            });

            _fakeUserRatings.Add(new UserRating
            {
                RaterUserID = 4,
                UserBeingRatedID = 2,
                Comment = "elbows too pointy",
                RatingID = 1
            });

            _fakeUserRatings.Add(new UserRating
            {
                RaterUserID = 2,
                UserBeingRatedID = 1,
                Comment = "Alright",
                RatingID = 3
            });

            _fakeUserRatings.Add(new UserRating
            {
                RaterUserID = 3,
                UserBeingRatedID = 4,
                Comment = "Fun Times",
                RatingID = 4
            });

            _fakeUserRatings.Add(new UserRating
            {
                RaterUserID = 4,
                UserBeingRatedID = 5,
                Comment = "5/5 would watch again",
                RatingID = 5
            });

            _fakeUserRatings.Add(new UserRating
            {
                RaterUserID = 5,
                UserBeingRatedID = 3,
                Comment = "Best stream ever",
                RatingID = 5
            });

            _fakeUserRatings.Add(new UserRating
            {
                RaterUserID = 3,
                UserBeingRatedID = 1,
                Comment = "boring",
                RatingID = 1
            });
        }
    }
}
