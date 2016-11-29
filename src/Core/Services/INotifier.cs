using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Services
{
    public interface INotifier
    {
        void SendAssignedNotification(string message, Employee employee);
        void SendChangeStateNotification(string message);

    }
}