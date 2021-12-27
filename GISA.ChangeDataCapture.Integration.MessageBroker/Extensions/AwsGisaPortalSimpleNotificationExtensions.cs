using GISA.ChangeDataCapture.MessageBroker.Contracts;
using GISA.ChangeDataCapture.MessageBroker.Models;
using GISA.ChangeDataCapture.MessageBroker.SNS;
using Microsoft.Extensions.DependencyInjection;

namespace GISA.ChangeDataCapture.MessageBroker.Extensions
{
    public static class AwsGisaPortalSimpleNotificationExtensions
    {
        public static IServiceCollection AddMessageBrokerSimpleNotification(this IServiceCollection services, AwsSimpleNotificationOptions awsSimpleNotificationOptions)
        {
            services.AddSingleton(_ => awsSimpleNotificationOptions);
            services.AddTransient<IChangeDataCaptureNotification, AwsGisaPortalSimpleNotification>();

            return services;
        }
    }
}
