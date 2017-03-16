using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Managers
{
    public class RatingManager : IDisposable
    {
        IBrothershipUnitOfWork _unitOfWork;

        public RatingManager()
        {
            _unitOfWork = new BrothershipUnitOfWork();
        }

        public RatingManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<UserRating> GetAll()
        {
            // TODO (TH) Make test data and functions.
            var allUserRatings = new List<UserRating>();
            allUserRatings = (List<UserRating>)_unitOfWork.UserRatings.GetAll();
            return allUserRatings;
        }

        public List<UserRating> GetAllByUserId(int ratedUserId)
        {
            // TODO (TH) Make test data and functions.
            var userRatings = new List<UserRating>();
            userRatings = (List<UserRating>)_unitOfWork.UserRatings.GetAllByUserId(ratedUserId);
            return userRatings;
        }

        public UserRating GetById(int raterId, int userBeingRatedId)
        {
            // TODO (TH) Make test data and functions.
            var userRating = _unitOfWork.UserRatings.GetById(raterId, userBeingRatedId);
            return userRating;
        }

        // TODO (TH) Might be a better and faster way to get average rating?
        /* (TH) There are mulitple ways to do this and I am unsure yet if we should calculate the average from the PL or the BL.
         * Also should we perhaps change the Rating.Description attribute to an Int value since it is simply 1-5 stars?
         * */
        public int GetAverageRating(int ratedUserId)
        {
            // TODO (TH) Make test data and functions.
            int averageRating = 0;
            int totalRating = 0;
            var userRatings = new List<UserRating>();
            userRatings = (List<UserRating>)_unitOfWork.UserRatings.GetAllByUserId(ratedUserId);

            foreach (var userRating in userRatings)
            {
                totalRating += Convert.ToInt32(userRating.Rating.Description);
            }

            averageRating = totalRating / userRatings.Count();

            return averageRating;
        }

    }
}
