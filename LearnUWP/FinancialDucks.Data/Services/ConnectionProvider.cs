using FinancialDucks.Data.Helpers;
using Microsoft.Data.SqlClient;
using OneConfig;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace FinancialDucks.Data.Services
{
    public interface IConnectionProvider
    {
        IDbConnection CreateConnection();
        bool IsLocal { get; }
    }

    public class SqlConnectionProvider : IConnectionProvider
    {
        public bool IsLocal => false;

        public IDbConnection CreateConnection()
        {
            var connectionString = AppConfig.GetValue("SqlConnection", required:true);
            return new SqlConnection(connectionString);
        }
    }

    public class SQLiteConnectionProvider : IConnectionProvider
    {
        public FileInfo SQLiteDBFile { get; private set; }
        private readonly SQLiteDatabaseCreator _dbCreator;

        public bool IsLocal => true;

        public SQLiteConnectionProvider(SQLiteDatabaseCreator dbCreator)
        {
            SQLiteDBFile = new FileInfo($@"{DataConfig.DataPath}\LocalDB.sqlite");
            _dbCreator = dbCreator;
        }

        public IDbConnection CreateConnection()
        {
            bool isNew = false;

            if (!SQLiteDBFile.Directory.Exists)            
                SQLiteDBFile.Directory.Create();
            

            var connectionString = $@"Data Source={SQLiteDBFile.FullName};";
            if (!File.Exists(SQLiteDBFile.FullName))
            {
                connectionString += "New=True;";
                isNew = true;
            }

            var connection = new SQLiteConnection(connectionString);

            if (isNew)
                _dbCreator.CreateDatabase(connection);
            

            return connection;
        }
    }
}
