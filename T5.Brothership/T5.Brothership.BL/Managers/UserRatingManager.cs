using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Managers
{
    public class UserRatingManager : IDisposable
    {
        IBrothershipUnitOfWork _unitOfWork;

        public UserRatingManager()
        {
            _unitOfWork = new BrothershipUnitOfWork();
        }

        public UserRatingManager(IBrothershipUnitOfWork unitOfWork)
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
            return _unitOfWork.UserRatings.GetAll().ToList();
        }

        public List<UserRating> GetAllByUserId(int ratedUserId)
        {
            // TODO (TH) Make test data and functions.
            return _unitOfWork.UserRatings.GetAllByUserId(ratedUserId).ToList();
        }

        public UserRating GetById(int raterId, int userBeingRatedId)
        {
            return _unitOfWork.UserRatings.GetById(raterId, userBeingRatedId);
        }

        public double GetAverageRating(int ratedUserId)
        {
            return Math.Round(_unitOfWork.UserRatings.GetAllByUserId(ratedUserId).Average(p => p.RatingID), 2);
        }

    }
}
