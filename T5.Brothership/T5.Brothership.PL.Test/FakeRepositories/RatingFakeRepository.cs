using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    internal class RatingFakeRepository : IRatingRepository
    {
        List<Rating> _fakeRatings = new List<Rating>();

        public RatingFakeRepository()
        {
            InitializedFakeRatings();
        }

        public void Add(Rating rating)
        {
            rating.ID = GenerateRatingId();
            _fakeRatings.Add(rating);
        }

        public void Delete(int id)
        {
            var rating = _fakeRatings.Single(p => p.ID == id);
            _fakeRatings.Remove(rating);
        }

        public void Delete(Rating entity)
        {
            var rating = _fakeRatings.Single(p => p.ID == entity.ID);
            _fakeRatings.Remove(rating);
        }

        public void Dispose()
        {
            _fakeRatings = null;
        }

        public IQueryable<Rating> GetAll()
        {
            return _fakeRatings.AsQueryable();
        }

        public Rating GetById(int id)
        {
            return _fakeRatings.FirstOrDefault(p => p.ID == id);
        }

        public void SaveChanges()
        {
        }

        public void Update(Rating entity)
        {
            var entityIndex = _fakeRatings.IndexOf(entity);
            _fakeRatings[entityIndex] = entity;
        }

        private int GenerateRatingId()
        {
            return _fakeRatings.Max(p => p.ID) + 1;
        }

        private void InitializedFakeRatings()
        {
            _fakeRatings.Add(new Rating
            {
                ID = 1,
                Description = "OneStar"
            });

            _fakeRatings.Add(new Rating
            {
                ID = 2,
                Description = "TwoStars"
            });

            _fakeRatings.Add(new Rating
            {
                ID = 3,
                Description = "ThreeStars"
            });

            _fakeRatings.Add(new Rating
            {
                ID = 4,
                Description = "FourStars"
            });

            _fakeRatings.Add(new Rating
            {
                ID = 5,
                Description = "FiveStars"
            });
        }
    }
}
