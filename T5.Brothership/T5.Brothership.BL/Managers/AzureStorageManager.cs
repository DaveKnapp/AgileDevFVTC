using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL;

namespace T5.Brothership.BL.Managers
{
    public class AzureStorageManager : IDisposable
    {
        IBrothershipUnitOfWork _unitOfWork;

        public AzureStorageManager()
        {
            _unitOfWork = new BrothershipUnitOfWork();
        }

        public AzureStorageManager(IBrothershipUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }

        public string UploadImage(User user, byte[] imageArray)
        {
            _unitOfWork.AzureBlobStorage.Upload(imageArray, user.UserName);
            return _unitOfWork.AzureBlobStorage.GetBlobUri(user.UserName);
        }
    }
}
