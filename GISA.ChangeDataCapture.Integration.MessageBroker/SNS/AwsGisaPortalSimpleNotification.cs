using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using GISA.ChangeDataCapture.MessageBroker.Contracts;
using GISA.ChangeDataCapture.MessageBroker.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace GISA.ChangeDataCapture.MessageBroker.SNS
{
    public class AwsGisaPortalSimpleNotification : IChangeDataCaptureNotification
    {
        private AmazonSimpleNotificationServiceClient _amazonSimpleNotificationServiceClient;
        private readonly ILogger<AwsGisaPortalSimpleNotification> _logger;
        private readonly AwsSimpleNotificationOptions _awsSimpleNotificationOptions;
        private JsonSerializerSettings _jsonSerializerSettings;

        private JsonSerializerSettings JsonSerializerSettings
        {
            get
            {
                if (_jsonSerializerSettings == null)
                {
                    _jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                    _jsonSerializerSettings.Converters.Add(new StringEnumConverter());
                }

                return _jsonSerializerSettings;
            }
        }

        private AmazonSimpleNotificationServiceClient AmazonSimpleNotificationServiceClient
        {
            get
            {
                if (_amazonSimpleNotificationServiceClient == null)
                    _amazonSimpleNotificationServiceClient = new AmazonSimpleNotificationServiceClient
                        (awsSecretAccessKey: _awsSimpleNotificationOptions.SecretKey,
                        awsAccessKeyId: _awsSimpleNotificationOptions.AccessKey,
                        region: Amazon.RegionEndpoint.GetBySystemName(_awsSimpleNotificationOptions.Region));

                return _amazonSimpleNotificationServiceClient;
            }
        }

        public AwsGisaPortalSimpleNotification(ILogger<AwsGisaPortalSimpleNotification> logger, AwsSimpleNotificationOptions awsSimpleNotificationOptions)
        {
            _logger = logger;
            _awsSimpleNotificationOptions = awsSimpleNotificationOptions;
        }

        public Task PublishAsync<T>(T instance)
        {
            try
            {
                return AmazonSimpleNotificationServiceClient.PublishAsync(
                    new PublishRequest
                    {
                        Message = JsonConvert.SerializeObject(instance, JsonSerializerSettings),
                        TopicArn = _awsSimpleNotificationOptions.TopicArn
                    });
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message} {exception?.InnerException.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
