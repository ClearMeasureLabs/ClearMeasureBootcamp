namespace ClearMeasure.Bootcamp.Core.Services
{
	public class FlashMessage
	{
		public enum MessageType
		{
			Message,
			Error
		}

		private MessageType _type;
		private string _message;

		public FlashMessage(MessageType type, string message)
		{
			_type = type;
			_message = message;
		}

		public MessageType Type
		{
			get { return _type; }
		}

		public string Message
		{
			get { return _message; }
		}
	}
}