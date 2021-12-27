namespace GISA.ChangeDataCapture.MessageBroker.Models
{
    public class AwsSimpleNotificationOptions
    {
        public string Region { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string TopicArn { get; set; }
    }
}
