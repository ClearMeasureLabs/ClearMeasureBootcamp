using System.Diagnostics;
using System.Reflection;

namespace ClearMeasure.Bootcamp.Core.Services.Impl
{
	public class ApplicationInformation : IApplicationInformation
	{
		private FileVersionInfo _versionInfo;
		private AssemblyName _assemblyName;

		public ApplicationInformation()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			_versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
			_assemblyName = assembly.GetName();
		}

		public string ProductName
		{
			get { return _versionInfo.ProductName; }
		}

		public string ProductVersion
		{
			get { return _assemblyName.Version.ToString(); }
		}

		public string ProductMajorMinorVersion
		{
			get { return string.Format("{0}.{1}", _versionInfo.ProductMajorPart, _versionInfo.ProductMinorPart); }
		}

		public int ProductBuildPart
		{
			get { return _versionInfo.ProductBuildPart; }
		}

		public int ProductPrivatePart
		{
			get { return _versionInfo.ProductPrivatePart; }
		}

		public string CompanyName
		{
			get { return _versionInfo.CompanyName; }
		}

		public string LegalCopyright
		{
			get { return _versionInfo.LegalCopyright; }
		}

		public string LegalTrademarks
		{
			get { return _versionInfo.LegalTrademarks; }
		}
	}
}