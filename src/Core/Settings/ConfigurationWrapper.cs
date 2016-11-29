using System.Configuration;

namespace ClearMeasure.Bootcamp.Core.Settings
{
    public class ConfigurationWrapper : IConfigurationWrapper
    {
        public string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}