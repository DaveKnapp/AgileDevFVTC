using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace T5.Brothership.PL.Repositories
{
    public interface IBlobRepository : IRepository<User>
    {
        string GetBlobImage(string username);

        string UploadProfileImage(string username, string filePath);

        CloudBlockBlob GetBlobInContainer(string username);
    }
}
