using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Data;

namespace LearnUWP.Converters
{
    public class EnumToStringFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var text = value.ToString();
            text = Regex.Replace(text, "([A-Z])([a-z])", " $1$2");
            return text.Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
