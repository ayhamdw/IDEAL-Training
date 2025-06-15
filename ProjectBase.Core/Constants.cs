namespace ProjectBase.Core
{
    public class Constants
    {       
      

        public static class Languages
        {
            public const string Arabic = "ar";
            public const string English = "en";
        }

        public static class EnvironmentVariables
        {
            public const string S3BucketNameKey = "BUCKET_NAME";
            public const string BucketNameDefualtValue = "";
            public const string AWSAccessKey = "AWS_ACCESS_KEY";
            public const string AWSAccessKeyDefualtValue = "";
            public const string AWSSecretAccessKey = "AWS_SECRET_ACCESS_KEY";
            public const string AWSSecretAccessKeyDefualtValue = "";
            public const string DBConnectionString = "DB_CONNECTION_STRING";
        }
        
        
        public static class SystemSettings
        {
            public const string EmailsSourceEmail = "ghadeerfuture3@gmail.com";
            public const string EmailsSmtpClient = "smtp.gmail.com";
            public const string EmailsReplyTo = "ghadeerfuture3@gmail.com";
            public const string EmailsSmtpPort = "587";
            public const string EmailsErrorEmail = "ghadeerfuture3@gmail.com";
            public const string EmailsPassword = "ucwq lkfb gzok jfly";
            public const string EmailSenderName = "Ghadeer Future E-Commerce";
        }

        public static class SortingDirection
        {
            public const string Ascending = "asc";
            public const string Descending = "desc";
        }

        public static class Urls
        {
            public const string ResetPasswordBaseUrl = "https://ddd5pfz6qm4m6.cloudfront.net/auth/reset-password";
            public const string profileImagePath = "https://img.freepik.com/free-vector/person-choosing-direction-illustration_24877-82864.jpg?t=st=1745999801~exp=1746003401~hmac=e3ea2ae268d946a9188c47f837b6757bf32d59df08b1822a9010343094623beb&w=740";
            public const string LoginPage = "https://ddd5pfz6qm4m6.cloudfront.net/auth/login";

        }


    }
}
