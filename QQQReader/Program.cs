using System;
using System.IO;
using Npgsql;

namespace QQQReader
{
	public class Program
	{
        /// <summary>
        /// Programme to insert all QQQ tickers into the database
        /// </summary>
        public static void Main()
        {

            QQQReader reader = new QQQReader();

            List<List<string>> pairs = reader.ReadQqqCsv();

            List<string> tickers = new List<string>();

            foreach (List<string> pair in pairs) // not great but it'll do. 
            {
                tickers.Add(pair[0].Replace("\'", ""));
            }

            File.WriteAllLines("QQQ_tickers.txt", tickers);

            //TODO: write a BULK INSERT....

            string queryString = String.Format("COPY \"tickersEnum\" (ticker) FROM {0}",
                "'/Users/matthewpotts/Projects/dotnet-alpaca-v2/QQQReader/bin/Debug/net6.0/QQQ_tickers.txt'");

            using (NpgsqlConnection connection = reader.SqlConnection)
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                     command.ExecuteNonQuery();
                }
            }
            
        }
    }
}

