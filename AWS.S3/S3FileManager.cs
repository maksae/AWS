using Amazon;
using Amazon.S3;
using Amazon.S3.IO;
using Amazon.S3.Model;
using System.Configuration;
using System.IO;

namespace AWS.S3
{
    public class S3FileManager
    {
        private readonly IAmazonS3 _client = new AmazonS3Client(RegionEndpoint.EUCentral1);
        private readonly string bucketName = ConfigurationManager.AppSettings["BucketName"];

        public string SendFileToBucket(string fileName, Stream fileStream)
        {
            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    InputStream = fileStream,
                    
                };

                PutObjectResponse response = _client.PutObject(putRequest);
                return string.Format("File {0} is uploaded to {1} (Status: {2})",
                    putRequest.Key, putRequest.BucketName, response.HttpStatusCode);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                return  ("Error: " + amazonS3Exception.Message);
            }
        }

        public string GetFileFromBucket(string fileName)
        {
            try
            {
                GetObjectRequest getRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

                GetObjectResponse response = _client.GetObject(getRequest);
                return string.Format("File {0} is downloaded from {1} (Status: {2})",
                    response.Key, response.BucketName, response.HttpStatusCode);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                return ("Error: " + amazonS3Exception.Message);
            }
        }

        public string DeleteFile(string fileName)
        {
            try
            {
                DeleteObjectRequest deleteRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

                DeleteObjectResponse response = _client.DeleteObject(deleteRequest);
                return string.Format("File {0} is deleted from {1} (Status: {2})",
                    deleteRequest.Key, deleteRequest.BucketName, response.HttpStatusCode);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                return ("Error: " + amazonS3Exception.Message);
            }
        }

        public string CheckFileinBucket(string fileName)
        {
            string message;
            var s3FileInfo = new S3FileInfo(_client, bucketName, fileName);

            if (s3FileInfo.Exists)
            {
                message = string.Format("File {0} exist in {1}", fileName, bucketName);
            }
            else
            {
                message = string.Format("File {0} does not exist in {1}", fileName, bucketName);
            }
            return message;
        }
    }
}
