using System.Collections.Generic;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Managers
{
    public interface IUserRatingManager
    {
        void Add(UserRating userRating);
        void Dispose();
        List<UserRating> GetAll();
        List<UserRating> GetAllByUserUserRatings(int ratedUserId);
        double GetAverageRating(int ratedUserId);
        UserRating GetById(int raterId, int userBeingRatedId);
    }
}