using System;
using System.Threading;
using Alpaca.Markets;
using System.Configuration;
using Npgsql;
using dotnet_alpaca_config;
using System.Collections.Generic;

namespace dotnet_alpaca_data
{
	public class Program
	{
		static SemaphoreSlim _pool;
		int numWaits = 0;

		public static async Task Main()
		{
			var barCollector = new CollectHistoricalBars();

			await barCollector.DoCollectBars();

			int? waitForCollectBars = null;

			while (waitForCollectBars != 0)
            {

				// DoCollectBars() fires off loads of processes, but not all are finished
				// before the thread starts moving ahead. This introduces a bit of static
				// to make sure that all bars are collected - hacky and probably a better
				// way of doing it. 

				int barCollectLenPre = barCollector.barList.Count;
				Console.WriteLine($"BarCollectLen Pre Wait: {barCollectLenPre}");

				Thread.Sleep(500);

				int barCollectLenPost = barCollector.barList.Count;
				Console.WriteLine($"BarCollectLen Post Wait: {barCollectLenPost}");

				waitForCollectBars = barCollectLenPost - barCollectLenPre; // when WaitForCollectBars = 0, no more bars processed. 
            }

			Console.WriteLine($"{barCollector.barList.Count} bars collected. Starting database insert...");

			try
			{
				DatabaseBarInsert.DoInsertBars(barCollector.barList);
			}
			catch (Exception e)
            {
				Console.WriteLine($"Exception caught: message was {e.Message}");
            }
		
		}
			
	}
}


