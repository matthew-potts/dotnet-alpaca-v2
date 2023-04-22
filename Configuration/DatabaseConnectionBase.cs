using System;
namespace dotnet_alpaca_config
{

	public interface IDatabaseConnectionBase
    {
		string ConnectionString { get; }
    }


	public class DatabaseConnectionBase : IDatabaseConnectionBase
	{
		private string _connectionString { get; }
		public string ConnectionString { get { return _connectionString; } }

		public DatabaseConnectionBase()
        {
			this._connectionString = DatabaseConnection.DatabaseConfiguration?.CONNECTION_STRING;
        }
	}
}

