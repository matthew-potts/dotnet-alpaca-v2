using System;
using System.Data;
using Npgsql;
using System.Data.SqlClient;
using Alpaca.Markets;

namespace dotnet_alpaca_data
{

	/// <summary>
	/// Class to insert a row of bars into the "bars" table of the DB.
	/// Called in CollectHistoricalBars.cs
	/// </summary>

	public static class DatabaseBarInsert
	{

		// will be adding to a _barList and then updating it only when
		// the _barList is done. Inefficient and can probably tidy it up later.

		static DataClient _dataClient;


		static DatabaseBarInsert()
		{
			_dataClient = new DataClient();

		}

		public static void DoInsertBars(List<IBar> bars)
		{

			var iCounter = 0;

			using (NpgsqlConnection _connection = _dataClient.SqlConnection)
			{
				_connection.Open();

				if (iCounter == 0)
				{
					NpgsqlTransaction transaction = _connection.BeginTransaction();


					foreach (IBar bar in bars)
					{
						//string query = $"INSERT INTO \"bar\"" +
						//                  $" (Timestamp, Ti	ckerId, Open, High, Low, Close) " +
						//                  $"VALUES " +

						using (NpgsqlCommand command = new NpgsqlCommand("insert_bar", _connection))
						{
							command.CommandType = CommandType.StoredProcedure;
							command.Parameters.AddWithValue("timeutc", bar.TimeUtc);
							command.Parameters.AddWithValue("symbol", bar.Symbol);
							command.Parameters.AddWithValue("open", bar.Open);
							command.Parameters.AddWithValue("high", bar.High);
							command.Parameters.AddWithValue("low", bar.Low);
							command.Parameters.AddWithValue("close", bar.Close);

							try
							{
								command.ExecuteNonQuery();
								Console.WriteLine($"Bar for symbol {bar.Symbol} at {bar.TimeUtc.ToShortDateString()} " +
									$"written successfully.");
								iCounter++;
							}

							catch (Exception ex)
							{
								Console.WriteLine($"Exception caught, message was: {ex.Message}");
								transaction.Rollback();
								break;
							}

						}

					}

					transaction.Commit();

					Console.WriteLine($"Processing finished: Inserted {iCounter} bars");
				}

				_connection.Close();


			}



		}

	}
}

