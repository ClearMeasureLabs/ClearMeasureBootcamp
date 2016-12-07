

namespace ClearMeasure.Bootcamp.Core.Services
{
    /// <summary>
    /// Information about an application
    /// </summary>
	public interface IApplicationInformation
	{
		string ProductName { get; }
		string ProductVersion { get; }
		string ProductMajorMinorVersion { get; }
		string CompanyName { get; }
		string LegalCopyright { get; }
		string LegalTrademarks { get; }
		int ProductBuildPart { get; }
		int ProductPrivatePart { get; }
	}
}