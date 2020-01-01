using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace LearnUWP.Services
{
    public static class DiagnosticsService
    {
        private static string Describe(this Control c)
        {
            if (string.IsNullOrEmpty(c.Name))
                return c.GetType().Name;
            else
                return $"{c.GetType().Name}:{c.Name}";
        }


        public static string Describe(this IEnumerable e, string delimiter=", ")
        {
            List<string> strings = new List<string>();

            foreach(var element in e)
                strings.Add(element?.ToString() ?? "(null)");

            return string.Join(delimiter, strings.ToArray());
        }

        public static string DescribeWithType(this object o)
        {
            if (o == null)
                return "(null)";
            else
                return $"{o.GetType().Name}:{o}";
        }

        public static void DebugWrite(this Control c, string message)
        {
#if DEBUG
            Debug.WriteLine($"{c.Describe()} {message}");
#endif
        }
    }
}
