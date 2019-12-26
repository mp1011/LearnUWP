using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialDucks.Data.Helpers
{
    public class SqlStringBuilder
    {
        private string tableName;
        private List<string> whereClauses = new List<string>();

        private SqlStringBuilder() { }
   
        public static SqlStringBuilder Select<T>()
        {
            var builder = new SqlStringBuilder();
            builder.tableName = $"{typeof(T).Name}s";
            return builder;
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
            sb.Append("SELECT * FROM ")
                .Append(tableName);

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
