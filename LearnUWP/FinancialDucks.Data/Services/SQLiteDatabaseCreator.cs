using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FinancialDucks.Data.Services
{
    public class SQLiteDatabaseCreator
    {
        public void CreateDatabase(SQLiteConnection connection)
        {
            foreach (var script in GetCreationScripts())
            {
                connection.Execute(script);
            }    
        }

        private IEnumerable<string> GetCreationScripts()
        {
            var resourceNames = typeof(SQLiteDatabaseCreator).Assembly
                                    .GetManifestResourceNames()
                                    .Where(p => p.Contains("SQLiteCreationScripts"))
                                    .ToArray();

            foreach (var name in resourceNames)
            {
                using (var stream = typeof(SQLiteDatabaseCreator).Assembly.GetManifestResourceStream(name))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        yield return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
