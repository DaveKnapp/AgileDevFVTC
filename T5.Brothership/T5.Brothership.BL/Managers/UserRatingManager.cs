using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Managers
{
    public class UserRatingManager : IDisposable, IUserRatingManager
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

        public List<UserRating> GetAllByUserUserRatings(int ratedUserId)
        {
            // TODO (TH) Make test data and functions.
            return _unitOfWork.UserRatings.GetAllByUserId(ratedUserId).ToList();
        }

        public void Add(UserRating userRating)
        {
            _unitOfWork.UserRatings.Add(userRating);
            _unitOfWork.Commit();
        }

        public UserRating GetById(int raterId, int userBeingRatedId)
        {
            return _unitOfWork.UserRatings.GetById(raterId, userBeingRatedId);
        }

        public double GetAverageRating(int ratedUserId)
        {
            var userRatings = _unitOfWork.UserRatings.GetAllByUserId(ratedUserId).ToList();

            if (userRatings.Count > 0)
            {
                return Math.Round(userRatings.Average(p => p.RatingID), 2);
            }
            else
            {
                return 0;
            }
        }

        public void Update(UserRating userRating)
        {
            var currentUserRating = _unitOfWork.UserRatings.GetById(userRating.RaterUserID, userRating.UserBeingRatedID);
            currentUserRating.Comment = userRating.Comment;
            currentUserRating.RatingID = userRating.RatingID;

            _unitOfWork.UserRatings.Update(currentUserRating);
            _unitOfWork.Commit();
        }
    }
}