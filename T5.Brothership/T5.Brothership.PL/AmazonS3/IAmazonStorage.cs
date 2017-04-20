using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;

namespace T5.Brothership.PL
{
    interface IAmazonStorage
    {
        string bucketName { get; }
        string userName { get; }

        string FindBucketLocation(IAmazonS3 client);

        void CreateABucket(IAmazonS3 client);

    }
}
