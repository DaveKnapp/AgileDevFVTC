using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;
using System.IO;
using System.Linq;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{
    [TestClass]
    public class AzureRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var dbContext = new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME))
            {
                SqlScriptRunner.RunAddTestDataScript(dbContext);
            }

        }

        [TestCategory("IntegrationTest"), TestMethod]
        public void UploadToBlob_WasImageArrayUploaded()
        {
            string directoryName = "test";
            string imagepath = @"Testimage\testimage.jpg";
            byte[] imgArray = ConvertImageToByte(imagepath);

            string blobName = string.Format(@"{0}_{1}.jpg", "profiletest", "testimage");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("brothership");
            container.CreateIfNotExists();

            CloudBlobDirectory directory = container.GetDirectoryReference(directoryName);

            CloudBlockBlob blockBlob = directory.GetBlockBlobReference(blobName);
            //blockBlob.Properties.ContentType = "image/jpg";
            //blockBlob.SetProperties();
            blockBlob.UploadFromByteArray(imgArray, 0, imgArray.Length);

        }

        public byte[] ConvertImageToByte(string imagepath)
        {    
            var imageObj = Image.FromFile(imagepath);
            MemoryStream ms = new MemoryStream();
            imageObj.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        [TestCategory("IntegrationTest"), TestMethod]
        public void BlobUri_DoesUploadMatchStorageUri()
        {
            var expectedUser = new User
            {
                Bio = "This is the Profile Image Blob Test",
                DateJoined = DateTime.Now,
                DOB = new DateTime(1990, 3, 2),
                Email = "expectedUser@gmail.com",
                NationalityID = 1,
                GenderId = 1,
                UserLogin = new UserLogin { PasswordHash = "PasswordTest", Salt = "none" },
                UserName = "TestUserOne",
                ProfileImagePath = " ",
                UserTypeID = 1
            };
            // The Profilekey will always be the word "profile" + "UserID"
            string profilekey = "profile" + expectedUser.ID;
            // The full BlobName will always be the profilekey + imagefile.jpg (the imagefile is always named after the user with lowercases)
            var blobName = string.Format(@"{0}_{1}.jpg", profilekey, expectedUser.UserName.ToLower());

            string actualBlobUri = GetBlobUri(expectedUser, profilekey);
            AssertBlobUri(expectedUser, actualBlobUri);

        }

        private void AssertBlobUri(User expected, string actualBlobUri)
        {
            string expectedBlobUri = @"https://brothership.blob.core.windows.net/brothership/profile0_testuserone.jpg"; // Not a real URI

            Assert.AreEqual(expectedBlobUri, actualBlobUri);
        }

        private string GetBlobUri(User expected, string profilekey)
        {
            var blobName = string.Format(@"{0}_{1}.jpg", profilekey, expected.UserName.ToLower());

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("brothership");
            container.CreateIfNotExists();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            return blockBlob.Uri.AbsoluteUri;

        }
    }
}
