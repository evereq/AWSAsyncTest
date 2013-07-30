using Amazon;

namespace Evereq.AWSAsyncTest
{
    public static class AWSConfig
    {
        public static string AwsAccessKeyId = ;
        public static string AwsSecretKey = ;
        public static string S3BucketName = ;
        public static string ServiceUrl = "http://s3-us-west-1.amazonaws.com"; // Should contain http://
        public static readonly RegionEndpoint RegionEndpoint = RegionEndpoint.USWest1;
        public static string S3Folder = "tmp";
    }
}
