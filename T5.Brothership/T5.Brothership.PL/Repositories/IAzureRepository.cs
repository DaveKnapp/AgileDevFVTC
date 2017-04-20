using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using T5.Brothership.Entities.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace T5.Brothership.PL.Repositories
{
    public interface IAzureRepository
    {
        
        void Upload(byte[] _imageArr, string username);

        string LoadBlob(string blobName);

        void Delete(string username);

        IEnumerable<CloudBlockBlob> GetAllBlobs(string username);

        string GetBlobUri(string username);

        string UploadProfileImage(string username, string filePath);

        CloudBlockBlob GetBlobInContainer(string username);


    }
}
