using ExchangeRates.Helpers;
using ExchangeRates.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ExchangeRates.Converters
{
    public class EnumToAttributeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is Enum enumValue)
            {
                if (parameter is string propertyName && !string.IsNullOrEmpty(propertyName))
                {
                    var attribute = enumValue.GetAttribute<EnumHelperAttribute>();
                    var pinfo = typeof(EnumHelperAttribute).GetProperty(propertyName);

                    if (attribute != null && pinfo != null)
                        return pinfo.GetValue(attribute);
                }
                
                return enumValue.ToString();
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
