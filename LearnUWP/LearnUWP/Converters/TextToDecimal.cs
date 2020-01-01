using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Data;

namespace LearnUWP.Converters
{
    public class TextToDecimal : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (targetType == typeof(Decimal))
            {
                var text = value.ToString();
                decimal decimalValue;
              
                if (decimal.TryParse(text, out decimalValue))
                    return decimalValue;
                else
                    return 0M;
            }
            else
                return value.ToString();
        }
    }
}
