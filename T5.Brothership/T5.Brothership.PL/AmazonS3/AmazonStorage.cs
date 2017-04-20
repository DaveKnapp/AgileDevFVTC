using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL.Repositories;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;

namespace T5.Brothership.PL
{
    public class AmazonStorage : IAmazonStorage
    {
        private string bucket;
        private string username;

        //public AmazonStorage()
        //{
        //    AmazonS3Client client = new AmazonS3Client();
        //}

        public AmazonStorage(string _bucket, string _username)
        {
            using (var client = new AmazonS3Client())
            {
                bucket = _bucket;
                username = _username;
                if (!(AmazonS3Util.DoesS3BucketExist(client, bucketName)))
                {
                    CreateABucket(client);
                }
                // Retrieve bucket location.
                string bucketLocation = FindBucketLocation(client);
            }
        }

        public string bucketName { get { return bucket; } }

        public string userName { get { return username; } }

        public void CreateABucket(IAmazonS3 client)
        {
            try
            {
                PutBucketRequest putRequest = new PutBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };

                PutBucketResponse response = client.PutBucket(putRequest);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new ArgumentException("Check the provided AWS Credentials.");
                }
                else
                {
                    throw new ArgumentException("Error occurred. Message:'{0}' when writing an objec", amazonS3Exception.Message);
                }
            }
        }

        public string FindBucketLocation(IAmazonS3 client)
        {
            string bucketLocation;
            GetBucketLocationRequest request = new GetBucketLocationRequest()
            {
                BucketName = bucketName
            };
            GetBucketLocationResponse response = client.GetBucketLocation(request);
            bucketLocation = response.Location.ToString();
            return bucketLocation;
        }

        public string UploadImageToBucket(IAmazonS3 client)
        {
            try
            {
                PutObjectRequest putRequest1 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    ContentBody = "sample text"
                };

                PutObjectResponse response1 = client.PutObject(putRequest1);

                // 2. Put object-set ContentType and add metadata.
                PutObjectRequest putRequest2 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath,
                    ContentType = "text/plain"
                };
                putRequest2.Metadata.Add("x-amz-meta-title", "someTitle");

                PutObjectResponse response2 = client.PutObject(putRequest2);

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                        "For service sign up go to http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine(
                        "Error occurred. Message:'{0}' when writing an object"
                        , amazonS3Exception.Message);
                }
            }

        }
    }
}
