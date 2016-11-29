
namespace ClearMeasure.Bootcamp.Core.Services
{
    public interface IEmailService
    {
        void SendMessage(string emailAddress, string message);
    }
}