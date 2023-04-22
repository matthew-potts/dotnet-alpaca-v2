using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace dotnet_alpaca_config
{
	public class ConfigurationBase
	{
        private static IConfigurationRoot? _configuration;
        public static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {

                    var configfile = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var path = configfile.FilePath;
                    configfile.AppSettings.File = "/Users/matthewpotts/Projects/dotnet-alpaca/Configuration/appSettings.json/";

                    var builder = new ConfigurationBuilder().AddJsonFile(configfile.AppSettings.File, optional: false, reloadOnChange: true);
                    _configuration = builder.Build();
                }

                return _configuration;

            }
        }
    }
}

