using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL;

namespace T5.Brothership.BL.Managers
{
    public class GenderManager: IDisposable
    {
        IBrothershipUnitOfWork _unitOfWork;

        public GenderManager()
        {
            _unitOfWork = new BrothershipUnitOfWork();
        }

        public GenderManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void Dispose()
        {
            _unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<Gender> GetAll()
        {
            return _unitOfWork.Genders.GetAll().ToList();
        }
    }
}
