namespace ClearMeasure.Bootcamp.Core.Settings
{
    public interface IConfigurationWrapper
    {
        string GetAppSetting(string key);
    }
}