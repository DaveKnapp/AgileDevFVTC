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
        private string connectionString = "StorageConnectionString"; // TODO (TH) - Make a more secure connection or a Shared Access Signature config instead.
        private string containerName = "brothership"; // This is the name of the primary container in Azure Storage.
        private string directoryName = "temp"; // The directory name for each user will always be set to their username.

        public AzureRepository()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
        }

        public string GetDefaultUserImage()
        {
            // Change this if you change the default image in storage.
            string blobName = string.Format("default-user.gif");
            return container.GetBlockBlobReference(blobName).Uri.AbsolutePath;
        }

        public string LoadBlob(string blobName)
        {
            throw new NotImplementedException();
        }

        public void Upload(byte[] _imageArr, User _user)
        {
            // This determines the identifying name of the blob in storage
            string blobName = string.Format(@"{0}_{1}.jpg", _user.ID, _user.UserName.ToLower());

            // Make a new directory and blob for the User.
            CloudBlobDirectory directory = container.GetDirectoryReference(directoryName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            // Upload the content to the blob
            blockBlob.UploadFromByteArray(_imageArr, 0, _imageArr.Length);
        }

        public void Delete(User _user)
        {
            string blobName = string.Format(@"{0}_{1}.jpg", _user.ID, _user.UserName.ToLower());

            if (BlobExistsOnCloud(_user.UserName.ToLower()))
            {
                // If the blob exists with that username, delete it.
                container.GetDirectoryReference(directoryName).GetBlockBlobReference(blobName).Delete(DeleteSnapshotsOption.IncludeSnapshots);
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
            var blobName = string.Format(@"{0}_{1}.jpg", containerName, username.ToLower());
            return container.GetBlockBlobReference(blobName).Exists();
        }

        // Use this to return the Blob URI to the User.ProfileImagePath
        public string GetBlobUri(User _user)
        {
            string blobName = string.Format(@"{0}_{1}.jpg", _user.ID, _user.UserName.ToLower());
            return container.GetDirectoryReference(directoryName).GetBlockBlobReference(blobName).Uri.AbsoluteUri;
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
