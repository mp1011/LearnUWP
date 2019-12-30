using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialDucks.Data.Helpers
{
    public class SqlStringBuilder
    {
        private string action;
        private string tableName;
        private List<string> whereClauses = new List<string>();

        private SqlStringBuilder() { }
   
        public static SqlStringBuilder Select<T>()
        {
            return Select(typeof(T));
        }

        public static SqlStringBuilder Delete<T>()
        {
            return Delete(typeof(T));
        }

        public static SqlStringBuilder Select(Type tableType)
        {
            var builder = new SqlStringBuilder();
            builder.action = "SELECT *"; 
            builder.tableName = GetTableName(tableType);
            return builder;
        }

        public static SqlStringBuilder Delete(Type tableType)
        {
            var builder = new SqlStringBuilder();
            builder.action = "DELETE";
            builder.tableName = GetTableName(tableType);
            return builder;
        }


        private static string GetTableName(Type tableType)
        {
            var nameAttr = tableType
                .GetCustomAttributes(false)
                .OfType<TableAttribute>()
                .FirstOrDefault();

            if (nameAttr != null)
                return nameAttr.Name;
            else 
                return $"{tableType.Name}s";
        }

        public SqlStringBuilder Where(string clause)
        {
            if(!string.IsNullOrEmpty(clause))
                whereClauses.Add(clause);
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{action} FROM {tableName}");

            if (whereClauses.Any())
            {
                sb.Append(" WHERE (");
                sb.Append(string.Join(") AND (", whereClauses.ToArray()));
                sb.Append(") ");
            }

            return sb.ToString();

        }
    }
}
