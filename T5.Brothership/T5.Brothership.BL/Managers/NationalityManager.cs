using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL;

namespace T5.Brothership.BL.Managers
{
    public class NationalityManager : IDisposable, INationalityManager
    {
        IBrothershipUnitOfWork _unitOfWork;

        public NationalityManager()
        {
            _unitOfWork = new BrothershipUnitOfWork();
        }

        public NationalityManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void Dispose()
        {
            _unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<Nationality> GetAll()
        {
            return _unitOfWork.Nationalities.GetAll().ToList();
        }
    }
}
