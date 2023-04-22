using System;
using Microsoft.Extensions.Configuration;
using Alpaca.Markets;
using dotnet_alpaca_config;

namespace dotnet_alpaca_trade
{
	public class AlpacaAccount
	{

		private IApiBase _apiBase;
		public AlpacaAccount()
		{
			_apiBase = new AlpacaAPIBase();
		}

		public async Task<IAccount> GetAccount()
        {
			var client = Environments.Paper.GetAlpacaTradingClient(new SecretKey(_apiBase.ClientKey, _apiBase.SecretKey));
			var account = await client.GetAccountAsync();
			return account;
		}




	}
}

