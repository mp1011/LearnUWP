using Dapper;
using Dapper.Contrib.Extensions;
using FinancialDucks.Data.Helpers;
using FinancialDucks.Data.Interfaces;
using System;
using System.Linq;

namespace FinancialDucks.Data.Services
{
    public class DAO
    {
        private readonly IConnectionProvider _connectionProvider;
        public DAO(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void Upsert<T>(T model)
            where T:class,IWithID
        {
            using (var conn = _connectionProvider.CreateConnection())
            {
                if(model.ID==0)
                    model.ID = (int)conn.Insert(model);
                else
                    conn.Update(model);
            }
        }

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
