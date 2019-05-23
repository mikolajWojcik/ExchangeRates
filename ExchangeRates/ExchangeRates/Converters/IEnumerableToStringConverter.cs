using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ExchangeRates.Converters
{
    public class IEnumerableToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is IEnumerable enumerable)
            {
                var stringBuilder = new StringBuilder();

                foreach(var item in enumerable)
                {
                    stringBuilder.Append(item.ToString());
                    stringBuilder.Append(", ");
                }
                stringBuilder.Remove(stringBuilder.Length-2, 2);

                return stringBuilder.ToString();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
