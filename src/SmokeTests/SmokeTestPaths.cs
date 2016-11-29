using System;
using System.Configuration;

namespace ClearMeasure.Bootcamp.SmokeTests
{
    public static class SmokeTestPaths
    {
        public static string GetIisExpressExecArguments()
        {
            // TODO: Think of a better, more robust way of finding the website root
            var path = AppDomain.CurrentDomain.BaseDirectory;
            // for running within VS
            path = path.Replace(@"SmokeTests\bin\Debug", "UI");
            // for running from the command line
            path = path.Replace(@"build\test", @"src\UI");
            var port = ConfigurationManager.AppSettings["port"];
            var arguments = $"/path:{path} /port:{port}";
            return arguments;
        }

        public static string GetIisExpressExecPath()
        {
            var path = ConfigurationManager.AppSettings["iisExpressPath"];
            return path;
        }

        public static string GetDriversPath()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug", "Drivers");
            return path;
        }

        public static string GetPhantomJsPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}