using System;

namespace FinancialDucks.Extensions
{
    public static class StringExtensions
    {

        public static string ResolveAppData(this string path)
        {
            path = path.Replace("%APPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            return path;
        }
    }
}
