using Dapper;
using FinancialDucks.Data.Helpers;
using Microsoft.Data.SqlClient;
using OneConfig;
using System.Data;
using System.Linq;
using System.Text;

namespace FinancialDucks.Data.Services
{
    public class DAO
    {
        private readonly IConnectionProvider _connectionProvider;
        public DAO(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }


        //todo, params
        public T[] Read<T>(string whereClause=null, object param=null)
        {
            var sql = SqlStringBuilder
                .Select<T>()
                .Where(whereClause);
     
            using (var conn = _connectionProvider.CreateConnection())
            {
                return conn
                    .Query<T>(sql.ToString(), param)
                    .ToArray();
            }
        }
    }
}
