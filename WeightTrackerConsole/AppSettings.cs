namespace WeightTrackerConsole
{
    using Microsoft.Extensions.Configuration;
    using System;

    public class AppSettings
    {
        public String ConnectionString {get;set;}
        public String TableName {get;set;}

        public static AppSettings LoadAppSettings()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("Settings.json")
                .Build();

                AppSettings appSettings = configurationRoot.Get<AppSettings>();

                return appSettings;
        }
    }    
}