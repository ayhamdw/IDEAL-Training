using Amazon.Rekognition.Model;
using Amazon.Rekognition;
using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using ProjectBase.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBase.Core;

namespace ProjectBase.Services.Services
{
    public class AwsServices : IAwsServices
    {
        private readonly string _bucketName = Constants.EnvironmentVariables.S3BucketNameKey;
        private readonly IAmazonS3 _s3Client;

        public AwsServices(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }


        public async Task<string> UploadToS3(string key, IFormFile image)
        {
            using var imageStream = image.OpenReadStream();

            var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key,
                    InputStream = imageStream
            };
                var s = await _s3Client.PutObjectAsync(putRequest);
            return $"https://{_bucketName}.s3.amazonaws.com/{key}";

        }

    }
    
}
