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

        public T Upsert<T>(T model)
            where T:class,IWithID
        {
            using (var conn = _connectionProvider.CreateConnection())
            {
                if (model.ID == 0)
                {
                    var insertResult = conn.Insert(model);
                    if (insertResult is long number)
                    {
                        model.ID = (int)number;
                        model.LocalID = (int)number;
                    }
                    if (_connectionProvider.IsLocal)
                        conn.Execute($"UPDATE {SqlStringBuilder.GetTableName<T>()} Set ID = {model.LocalID} WHERE LocalID={model.LocalID}");
                }
                else
                    conn.Update(model);

            }

            return model;
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

        public void Delete<T>(string whereClause=null, object param=null)
        {
            var sql = SqlStringBuilder
               .Delete<T>()
               .Where(whereClause);

            using (var conn = _connectionProvider.CreateConnection())
            {
                conn.Execute(sql.ToString(), param);
            }
        }
    }
}
