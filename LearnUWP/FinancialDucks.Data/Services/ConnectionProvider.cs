using Microsoft.Data.SqlClient;
using OneConfig;
using System.Configuration;
using System.Data;

namespace FinancialDucks.Data.Services
{
    public interface IConnectionProvider
    {
        IDbConnection CreateConnection();
    }

    public class ConnectionFromAppSettingsProvider : IConnectionProvider
    {
        public IDbConnection CreateConnection()
        {
            var connectionString = AppConfig.GetValue("SqlConnection", required:true);
            return new SqlConnection(connectionString);
        }
    }
}
