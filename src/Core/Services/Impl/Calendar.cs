using System;

namespace ClearMeasure.Bootcamp.Core.Services.Impl
{
	public class Calendar : ICalendar
	{
		public DateTime GetCurrentTime()
		{
			return DateTime.Now;
		}
	}
}