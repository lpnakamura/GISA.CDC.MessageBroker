using System.Threading.Tasks;

namespace GISA.ChangeDataCapture.MessageBroker.Contracts
{
    public interface IChangeDataCaptureNotification
    {
        Task PublishAsync<T>(T instance);
    }
}
