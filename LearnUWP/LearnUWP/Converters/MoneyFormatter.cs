using System;
using Windows.UI.Xaml.Data;

namespace LearnUWP.Converters
{
    public class MoneyFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(targetType == typeof(string))
            {
                if(value is decimal decimalValue)
                    return decimalValue.ToString("C");    
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
