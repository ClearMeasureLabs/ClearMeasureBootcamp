
namespace ClearMeasure.Bootcamp.Core.Services.Impl
{
    public class EmailService : IEmailService
    {
        private IUserSession _session;

        public EmailService(IUserSession session)
        {
            _session = session;
        }

        public void SendMessage(string emailAddress, string message)
        {
            string flashMessage = string.Format("'{0}' sent to '{1}'", message, emailAddress);
            FlashMessage flash = new FlashMessage(FlashMessage.MessageType.Message, flashMessage);
           _session.PushUserMessage(flash);
        }
    }
}