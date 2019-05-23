using ExchangeRates.Helpers;
using ExchangeRates.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace ExchangeRates.Converters
{
    public class CurrencyTypeFlagImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is Enum enumValue)
            {
                var attribute = enumValue.GetAttribute<EnumHelperAttribute>();

                if (attribute != null)
                {
                    var imageName = attribute.FlagImage;
                    var source = $"ExchangeRates.Resources.Flags.{imageName}";

                    return ImageSource.FromResource(source, typeof(CurrencyTypeFlagImageConverter).GetTypeInfo().Assembly);
                }                 
            }

            return default(ImageSource);           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
