using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace T5.Brothership.PL.Repositories
{
    public class BlobRepository : BaseRepositoy<User>, IBlobRepository
    {
        public BlobRepository(DbContext dbContext) : base(dbContext)
        {
            
        }

        public string GetBlobImage(string username)
        {
            CloudBlockBlob blob = GetBlobInContainer(username);
            return ("<img src=" + blob.Uri.AbsoluteUri + " alt='PS Image'>");
        }

        public CloudBlockBlob GetBlobInContainer(string username)
        {
            //Parse the connection. (found in app.config)
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //Create the blob client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Retrieve a refernce to a container
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(CloudConfigurationManager.GetSetting(username));

            //Set permission to show to public
            blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            blobContainer.CreateIfNotExists();

            // Retrieve reference to a blob name.
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference("profile-image");
            return blockBlob;
        }

        public string UploadProfileImage(string username, string filePath)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = blobClient.GetContainerReference(CloudConfigurationManager.GetSetting(username));
            blobContainer.CreateIfNotExists();

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference("profile-image");

            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            // This may not be needed later. For now this is here to give me the blobURI to store in User.ProfileImagePath until I am positive the storage is working.
            return ("<img src=" + blockBlob.Uri.AbsoluteUri + " alt='PS Image'>");
        }
    }
}
