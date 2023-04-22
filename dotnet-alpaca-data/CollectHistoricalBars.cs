using System;
using Alpaca.Markets;
using Npgsql;
using System.Linq;

namespace dotnet_alpaca_data
{
	public class CollectHistoricalBars
	{
		public SemaphoreSlim _pool;
		public List<IBar> barList;
		public List<string> arr;


		public async Task<List<IBar>> DoCollectBars()
		{
			var dataClient = new DataClient();

			arr = GetFullTickers();


			barList = new List<IBar>();

			_pool = new SemaphoreSlim(20, 20);	

			foreach (string ticker in arr)
			{
				_pool.Wait();
				ThreadPool.QueueUserWorkItem(ProcessTicker, ticker);
			}

			return barList;	

		
		}

		public async void ProcessTicker(object state)
		{
			string ticker = (string)state;
			var dataClient = new DataClient()	;
			try
			{
				IPage<IBar> bars = await dataClient.GetHistoricalBars(ticker);
				foreach (var bar in bars.Items)
					barList.Add(bar);
			}

			catch (Exception ex)
			{
				Console.WriteLine($"Caught exception, message was {ex.Message}");
			}

			if (barList.Count % 50 == 0)
				Console.WriteLine($"Processed {barList.Count} bars");

			_pool.Release();

		}

		public void TestLinq(List<IBar> bars)
        {
			// make a copy of the list, so don't get the "Collection was modified" exception.
			// Need some way of the main programme stop calling TestLinq
			// when the async task to add stuff to the list is finished.

			bars = bars.ToList();

			IEnumerable<string> tickerQuery =
				from bar in bars
				where bar.Close > 90
				select bar.Symbol;

			foreach (string ticker in tickerQuery)
            {
				Console.WriteLine($"ticker is {ticker}");
            }
        }

		public List<string> GetFullTickers()
		{
			List<string> result = new List<string>();
			var dataClient = new DataClient();

			string query = "SELECT ticker FROM \"tickersEnum\"";

			using (NpgsqlConnection connection = dataClient.SqlConnection)
			{
				connection.Open();

				using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
				{
					NpgsqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						string ticker = (string)reader["ticker"];
						result.Add(ticker);
					}
				}
			}

			return result;
		}
	}
}

