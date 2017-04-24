using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL;

namespace T5.Brothership.BL.Managers
{
    public class RatingManager : IDisposable
    {
        GameManager _gameManager;
        IBrothershipUnitOfWork _unitOfWork;

        public RatingManager()
        {
            _unitOfWork = new BrothershipUnitOfWork();
            _gameManager = new GameManager(_unitOfWork);
        }

        public RatingManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _gameManager = new GameManager(_unitOfWork);
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<Rating> GetAll()
        {
            return _unitOfWork.Ratings.GetAll().ToList();
        }
    }
}
