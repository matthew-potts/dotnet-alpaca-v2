using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
namespace dotnet_alpaca_config
{
	public class DatabaseConnection
	{

		private static DatabaseConfig? _databaseConfiguration;
		public static DatabaseConfig DatabaseConfiguration
        {
            get
            {
                if (_databaseConfiguration == null)
                {
                    _databaseConfiguration = new DatabaseConfig();
                    ConfigurationBase.Configuration.Bind("DatabaseConfig", _databaseConfiguration);
                }

                return _databaseConfiguration;
            }
           
        }
    }
}

