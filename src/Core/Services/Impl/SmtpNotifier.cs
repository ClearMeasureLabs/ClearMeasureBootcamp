using System.IO;
using System.Net.Mail;
using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Services.Impl
{
	public class SmtpNotifier : INotifier
	{
		private SmtpClient _smtpClient = new SmtpClient();

		public void Send(string emailAddress, string emailText)
		{
			MailMessage message = new MailMessage("no-reply@google.com", emailAddress);
			message.Subject = "notification";
			message.IsBodyHtml = true;
			message.Body = emailText;

			sendMailMessage(message, _smtpClient);
		}

		protected virtual void sendMailMessage(MailMessage message, SmtpClient client)
		{
			client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
			client.PickupDirectoryLocation = Directory.GetCurrentDirectory();
			client.Send(message);
		}

        #region INotifier Members


        public void Notify(ExpenseReport expenseReport, Employee employee)
        {
            //throw new System.NotImplementedException("tbd");
        }

        public void Tweet(ExpenseReport expenseReport)
        {
            throw new System.NotImplementedException("tbd");
        }

        #endregion

	    public void SendAssignedNotification(string message, Employee employee)
	    {
	        
	    }

	    public void SendChangeStateNotification(string message)
	    {
	        
	    }
	}
}