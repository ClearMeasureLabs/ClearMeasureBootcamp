using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Services
{
    public class LoggingNotifier : INotifier 
    {
        public void SendAssignedNotification(string message, Employee employee)
        {
//            Log.Info(this, message);
        }

        public void SendChangeStateNotification(string message)
        {
//            Log.Info(this, message);
        }
    }
}