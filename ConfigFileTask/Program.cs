using System;
using System.Configuration;

namespace ConfigFileTask
{
    internal class Program
    {
        private static void Main()
        {
            ReadSetting("SiteUrl");

            Console.ReadKey();
        }

        private static void ReadSetting(string key)
        {
            try
            {
                var url = ConfigurationManager.AppSettings[key] ?? "Not found";

                Console.WriteLine("Site: " + url);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app setting");
            }
        }
    }
}
