using System;
namespace dotnet_alpaca_config
{

	public interface IApiBase
    {
		string ClientKey { get; }
		string SecretKey { get; }
    }

	public class AlpacaAPIBase : IApiBase
	{

		private string _clientKey;
		public string ClientKey { get { return _clientKey; } }

		private string _secretKey;
		public string SecretKey { get { return _secretKey; } }


		public AlpacaAPIBase()
		{
			this._clientKey = AlpacaPaperTradingConfiguration.AlpacaApiConfiguration?.API_KEY;
			this._secretKey = AlpacaPaperTradingConfiguration.AlpacaApiConfiguration?.SECRET_KEY_ID;
		}
	}
}
