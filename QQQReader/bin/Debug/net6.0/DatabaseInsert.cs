using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using dotnet_alpaca_config;

using Npgsql;

namespace dotnet_alpaca_data
{

    public class DatabaseInsert
    {
        IDatabaseConnectionBase _databaseConnection;

        public DatabaseInsert()
        {
            _databaseConnection = new DatabaseConnectionBase();
        }


        public void InsertValues(string tableName, List<string> values)
        {
            NpgsqlConnection sqlConnection = new NpgsqlConnection(_databaseConnection.ConnectionString); // if initialise sqlConnection outside the method, get CS0236: " a field
                                                                                                         // initialiser cannot be used to reference the nonstatic field _databaseConnection."
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            foreach (string valSet in values)
            {
                string queryString = $"INSERT INTO \"{tableName}\"" +
                $" VALUES ({valSet});";

                NpgsqlCommand sqlCommand = new NpgsqlCommand(queryString, sqlConnection);

                NpgsqlDataReader reader = sqlCommand.ExecuteReader();

                reader.Close();
            }
        }

        public static void InsertValues(string cols, string tableName, List<string> values, NpgsqlConnection sqlConnection)
        {

            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            foreach (string valSet in values)
            {
                string queryString = $"INSERT INTO \"{tableName}\"" +
                $"({cols}) VALUES ({valSet});";

                NpgsqlCommand sqlCommand = new NpgsqlCommand(queryString, sqlConnection);

                NpgsqlDataReader reader;

                try
                {
                     reader = sqlCommand.ExecuteReader();
                }
                catch (PostgresException pg)
                {
                    continue;
                }

                reader.Close();
                
            }
        }

 
        /* TO DO: Write an Execute function */
    }
}

