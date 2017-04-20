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
    public class AzureRepository : IAzureRepository
    {
        private CloudBlobContainer container;
        private string connectionString = "AzureStorageAccount"; // TODO (TH) - Make a more secure connection or a Shared Access Signature config instead.
        private string containerName = "brothership"; // This is the name of the primary container in Azure Storage.

        public AzureRepository()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(connectionString));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
        }

        public string LoadBlob(string blobName)
        {
            throw new NotImplementedException();
        }

        public void Upload(byte[] _imageArr, string username)
        {
            // This determines the identifying name/key of the blob in storage
            var blobName = string.Format(@"{0}\{1}.jpg", containerName, username.ToLower());

            // Make a new blob for the User.
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            // Declare the content type
            blockBlob.Properties.ContentType = "image/jpg";
            blockBlob.SetProperties(); ;

            // Upload the content to the blob
            blockBlob.UploadFromByteArray(_imageArr, 0, _imageArr.Length);
        }

        public void Delete(string username)
        {
            var blobName = string.Format(@"{0}\{1}.jpg", containerName, username.ToLower());

            if (BlobExistsOnCloud(username.ToLower()))
            {
                // If the blob exists with that username, delete it.
                container.GetBlockBlobReference(blobName).Delete(DeleteSnapshotsOption.IncludeSnapshots);
            }
        }

        // (TH) Don't plan to use this yet.
        public IEnumerable<CloudBlockBlob> GetAllBlobs(string username)
        {
            throw new NotImplementedException();
        }

        // Checks if a Blob of the same name already exists
        private bool BlobExistsOnCloud(string username)
        {
            var blobName = string.Format(@"{0}\{1}.jpg", containerName, username.ToLower());
            return container.GetBlockBlobReference(blobName).Exists();
        }

        // Use this to return the Blob URI to the User.ProfileImagePath
        public string GetBlobUri(string username)
        {
            var blobName = string.Format(@"{0}\{1}.jpg", containerName, username.ToLower());
            return container.GetBlockBlobReference(blobName).Uri.AbsoluteUri;
        }

        //------------------------------- (TH) Old Code. Not being used at the moment. Maybe delete later. -------------------------------
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
      
        // (TH) Old Code. Not being used at the moment. Maybe delete later.
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

            return ("<img src=" + blockBlob.Uri.AbsoluteUri + " alt='PS Image'>");
        }


    }
}
