using System;

namespace ClearMeasure.Bootcamp.Core.Services.Impl
{
	public class NumberGenerator : INumberGenerator
	{
		public string GenerateNumber()
		{
			return Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
		}
	}
}