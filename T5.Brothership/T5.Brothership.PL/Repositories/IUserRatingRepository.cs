using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Repositories
{
    public interface IUserRatingRepository
    {
        IQueryable<UserRating> GetAll();
        UserRating GetById(int raterId, int userBeingRatedId);
        IQueryable<UserRating> GetAllUserRatings(int ratedUserId);
        void Add(UserRating entity);
        void Update(UserRating entity);
        void Delete(UserRating entity);
        void Delete(int raterId, int userBeingRatedId);
        void SaveChanges();
    }
}
