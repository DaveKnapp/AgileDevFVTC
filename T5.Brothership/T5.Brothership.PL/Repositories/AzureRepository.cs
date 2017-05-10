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
        private string directoryName = "temp"; // Directories help divide each user into their own virtual folder.

        public AzureRepository()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(connectionString));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
        }

        public string GetDefaultUserImage()
        {
            // Change this if you change the default image in storage.
            string blobName = string.Format("default-user.gif");
            return container.GetBlockBlobReference(blobName).Uri.AbsoluteUri;
        }

        public string LoadBlob(string blobName)
        {
            throw new NotImplementedException();
        }

        public string Upload(byte[] _imageArr, User _user)
        {
            // This determines the identifying names of the blob in storage
            directoryName = _user.UserName;
            string blobName = string.Format(@"{0}_{1}.jpg", _user.ID, _user.UserName.ToLower());

            // Make a new directory and blob for the User.
            CloudBlobDirectory directory = container.GetDirectoryReference(directoryName);
            
            CloudBlockBlob blockBlob = directory.GetBlockBlobReference(blobName);
            blockBlob.Properties.ContentType = "image/jpg";

            // Upload the content to the blob
            blockBlob.UploadFromByteArray(_imageArr, 0, _imageArr.Length);

            return GetBlobUri(_user).ToString();
        }

        public void Delete(User _user)
        {
            directoryName = _user.UserName;
            string blobName = string.Format(@"{0}_{1}.jpg", _user.ID, _user.UserName.ToLower());

            if (BlobExistsOnCloud(_user))
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
        public bool BlobExistsOnCloud(User _user)
        {
            directoryName = _user.UserName;
            var blobName = string.Format(@"{0}_{1}.jpg", _user.ID, _user.UserName.ToLower());
            return container.GetDirectoryReference(directoryName).GetBlockBlobReference(blobName).Exists();
        }


        // Use this to return the Blob URI to the User.ProfileImagePath
        public string GetBlobUri(User _user)
        {
            directoryName = _user.UserName;
            string blobName = string.Format(@"{0}_{1}.jpg", _user.ID, _user.UserName.ToLower());
            var blockBlob = container.GetDirectoryReference(directoryName).GetBlockBlobReference(blobName);
            blockBlob.Properties.ContentType = "image/jpg";
            return blockBlob.Uri.AbsoluteUri;
        }
               
    }
}
