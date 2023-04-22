using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;


namespace dotnet_alpaca_config
{
	public class AlpacaPaperTradingConfiguration
	{       
        private static AlpacaApiConfig? _alpacaApiConfiguration;
        public static AlpacaApiConfig AlpacaApiConfiguration
        {
            get
            {
                if (_alpacaApiConfiguration == null)
                {
                    _alpacaApiConfiguration = new AlpacaApiConfig();
                    ConfigurationBase.Configuration.Bind("AlpacaKeys", _alpacaApiConfiguration);
                }

                return _alpacaApiConfiguration;
            }
        }
	}
}

