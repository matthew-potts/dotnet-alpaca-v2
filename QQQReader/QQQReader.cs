using System;
using System.Data;
using Npgsql;
using dotnet_alpaca_config;
using Alpaca;
using dotnet_alpaca_data;

namespace QQQReader
{

    public class QQQReader
    {

        private IDatabaseConnectionBase _databaseConnection;
        private IQQQReaderBase _qqqReader;
        public QQQReader()
        {

            _databaseConnection = new DatabaseConnectionBase();
            _qqqReader = new QQQReaderBase();        
        }

        private NpgsqlConnection _sqlConnection;
        public NpgsqlConnection SqlConnection
        {
            get
            {
                return new NpgsqlConnection(_databaseConnection.ConnectionString);  // I repeat this, so maybe refactor later.
            }
        }

        public string QQQFile
        {
            get
            {
                return _qqqReader.QQQFile;
            }
        }


        //public void InsertValues(string tableName, List<string> values, NpgsqlConnection npgsqlConnection)
        //{


        //    foreach (string valSet in values)
        //    {

        //        npgsqlConnection.Open();


        //        string queryString = $"INSERT INTO \"{tableName}\"" +
        //        $" VALUES ({valSet});";


        //        NpgsqlCommand sqlCommand = new NpgsqlCommand(queryString, npgsqlConnection);

        //        NpgsqlDataReader reader = sqlCommand.ExecuteReader();

        //        reader.Close();

        //        npgsqlConnection.Close();
        //    }
        //}

        //public void InsertValues(string cols, string tableName, List<string> values, NpgsqlConnection npgsqlConnection)
        //{

        //    foreach (string valSet in values)
        //    {
        //        npgsqlConnection.Open();


        //        string queryString = $"INSERT INTO \"{tableName}\"" +
        //        $"({cols}) VALUES ({valSet});";

        //        NpgsqlCommand sqlCommand = new NpgsqlCommand(queryString, SqlConnection);

        //        NpgsqlDataReader reader = sqlCommand.ExecuteReader();
               
        //        reader.Close();

        //        npgsqlConnection.Close();

        //    }
        //}

        public List<List<string>> ReadQqqCsv()
        {
            List<List<string>> tickerList;
            using (StreamReader sr = new StreamReader(_qqqReader.QQQFile))
            {
                tickerList = new List<List<string>>();
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    var ticker = line.Split(",")[2].Trim();
                    string modifiedTicker = $"'{ticker}'";

                    string company = line.Split(",")[6].Trim();

                    tickerList.Add(new List<string>() { modifiedTicker.ToUpper(), company });
                }

                return tickerList;
            }
        }

        

    }
}