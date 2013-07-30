using Amazon;

namespace Evereq.AWSAsyncTestOld
{
    public static class AWSConfig
    {
        public static string AwsAccessKeyId = ;
        public static string AwsSecretKey = ;
        public static string S3BucketName = ;
        public static string ServiceUrl = "s3-us-west-1.amazonaws.com"; // should NOT contain http://
        public static readonly RegionEndpoint RegionEndpoint = RegionEndpoint.USWest1;
        public static string S3Folder = "tmp";
    }
}
