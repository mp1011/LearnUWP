using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace LearnUWP.Services
{
    public static class DiagnosticsService
    {

        private static string Describe(this object o)
        {
            if (o is Control c)
                return c.Describe();
            else if (o == null)
                return "(null)";
            else 
                return o.GetType().Name;

        }

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

        public static void DebugWrite(this object o, string message)
        {
#if DEBUG
            Debug.WriteLine($"{o.Describe()} {message}");
#endif
        }
    }
}
