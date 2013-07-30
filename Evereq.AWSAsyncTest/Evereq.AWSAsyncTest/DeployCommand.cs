using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace Evereq.AWSAsyncTest
{
    public class DeployCommand
    {
      
        private AmazonS3Client GetAWSClient()
        {
            var config = new AmazonS3Config
            {
                UseHttp = true,
                ServiceURL = AWSConfig.ServiceUrl,
                RegionEndpoint = AWSConfig.RegionEndpoint
            };

            return new AmazonS3Client(AWSConfig.AwsAccessKeyId, AWSConfig.AwsSecretKey, config);
        }
   
        /// <summary>
        /// Upload local file with given s3Path to S3
        /// </summary>
        /// <param name="filesystemPath">Path to file (includes filename) in local file system</param>
        /// <param name="s3Path">Path to file (folder) in S3</param>
        /// <param name="fileName">Name of file in S3</param>
        private async Task UploadFileAsync(string filesystemPath, string s3Path, string fileName)
        {
            Console.WriteLine("Uploading Async {0} to Amazon S3", fileName);

            using (var ms = new MemoryStream())
            {
                using (var file = new FileStream(filesystemPath, FileMode.Open, FileAccess.Read))
                {
                    var bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);
                }

                var putObjectRequest = new PutObjectRequest
                {
                    CannedACL = S3CannedACL.PublicRead,
                    Key = s3Path + @"/" + fileName,
                    BucketName = AWSConfig.S3BucketName,
                    InputStream = ms
                };

                putObjectRequest.Headers["Content-Encoding"] = "gzip";
                putObjectRequest.Headers["Cache-Control"] = "max-age=31536000, public";

                using (var s3ClientExt = GetAWSClient())
                {
                    try
                    {
                        await s3ClientExt.PutObjectAsync(putObjectRequest).ConfigureAwait(continueOnCapturedContext: false);
                        Console.WriteLine("{0} was uploaded to S3", fileName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Async Upload for {0} FAILS with exception '{1}'", fileName, e.Message);
                        // throw;
                    }
                }

            }
        }

        /// <summary>
        /// Upload local file with given s3Path to S3
        /// </summary>
        /// <param name="filesystemPath">Path to file (includes filename) in local file system</param>
        /// <param name="s3Path">Path to file (folder) in S3</param>
        /// <param name="fileName">Name of file in S3</param>
        private void UploadFile(string filesystemPath, string s3Path, string fileName)
        {
            Console.WriteLine("Uploading normally {0} to Amazon S3", fileName);

            using (var ms = new MemoryStream())
            {
                using (var file = new FileStream(filesystemPath, FileMode.Open, FileAccess.Read))
                {
                    var bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);
                }

                var putObjectRequest = new PutObjectRequest
                {
                    CannedACL = S3CannedACL.PublicRead,
                    Key = s3Path + @"/" + fileName,
                    BucketName = AWSConfig.S3BucketName,
                    InputStream = ms
                };

                putObjectRequest.Headers["Content-Encoding"] = "gzip";
                putObjectRequest.Headers["Cache-Control"] = "max-age=31536000, public";

                using (var s3ClientExt = GetAWSClient())
                {
                    try
                    {
                        s3ClientExt.PutObject(putObjectRequest);

                        Console.WriteLine("{0} was uploaded to S3", fileName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Normal Upload for {0} FAILS with exception '{1}'", fileName, e.Message);
                        // throw;
                    }
                }

            }
        }


        public async Task StartAsync()
        {
            var sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "w.css");            
            var destPath = AWSConfig.S3Folder;
            var fileName = "w.css";

            await UploadFileAsync(sourcePath, destPath, fileName).ConfigureAwait(continueOnCapturedContext: false);            

            Console.Out.WriteLine("Deploy Async of {0} Finished", fileName);            
        }

        public void Start()
        {
            var sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "w.css");
            var destPath = AWSConfig.S3Folder;
            var fileName = "w.css";

            UploadFile(sourcePath, destPath, fileName);

            Console.Out.WriteLine("Deploy Normal of {0} Finished", fileName);
        }

    }
    
}
