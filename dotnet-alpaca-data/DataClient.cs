using System;
using Alpaca.Markets;
using Npgsql;
using dotnet_alpaca_config;

namespace dotnet_alpaca_data
{
	public class DataClient
	{
		private IApiBase _api;
		private IDatabaseConnectionBase _databaseConnection;

		public DataClient()
		{
			_api = new AlpacaAPIBase();
			_databaseConnection = new DatabaseConnectionBase();
		}

		private IAlpacaDataClient _client;
		public IAlpacaDataClient AlpacaDataClient()
        {
			_client = Environments.Paper.GetAlpacaDataClient(new SecretKey(_api.ClientKey, _api.SecretKey));
			return _client;
        }

		public NpgsqlConnection SqlConnection
        {
			get
			{
				return new NpgsqlConnection(_databaseConnection.ConnectionString);
			}
        }


		public async Task<IPage<IBar>> GetHistoricalBars(string ticker)
        {	
			var to = DateTime.Today;
			var from = to.AddDays(-5);

			var bars = await AlpacaDataClient().ListHistoricalBarsAsync(
				new HistoricalBarsRequest(ticker, from, to, BarTimeFrame.Day));

			return bars;
		}



	}
}
