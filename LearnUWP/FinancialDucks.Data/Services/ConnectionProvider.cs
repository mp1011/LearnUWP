using Microsoft.Data.SqlClient;
using OneConfig;
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
            var connectionString = AppConfig.GetValue("SqlConnection");
            return new SqlConnection(connectionString);
        }
    }
}
