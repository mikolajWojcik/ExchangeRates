using ExchangeRates.Helpers;
using ExchangeRates.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ExchangeRates.Converters
{
    public class EnumToDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is Enum enumValue)
            {
                var attribute = enumValue.GetAttribute<EnumHelperAttribute>();

                return attribute != null ? attribute.DisplayName : enumValue.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
