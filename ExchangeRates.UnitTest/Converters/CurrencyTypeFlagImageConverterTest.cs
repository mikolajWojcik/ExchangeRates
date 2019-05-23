using ExchangeRates.Converters;
using ExchangeRates.Helpers;
using ExchangeRates.Models.Attributes;
using ExchangeRates.Models.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace ExchangeRates.Test.Converters
{
    public class CurrencyTypeFlagImageConverterTest
    {
        private readonly CurrencyTypeFlagImageConverter _converter;

        public CurrencyTypeFlagImageConverterTest()
        {
            _converter = new CurrencyTypeFlagImageConverter();
        }

        [Test]
        public void ShouldReturnImageSourceForCurrencyType()
        {
            var testEnum = CurrencyType.CZK;

            var actualImageSource = (ImageSource)_converter.Convert(testEnum, null, null, null);

            Assert.IsNotNull(actualImageSource);
        }

        [Test]
        public void ShouldReturnNullWhenHasNoAttribute()
        {
            var testEnum = DayOfWeek.Monday;

            var actualObject = _converter.Convert(testEnum, null, null, null);

            Assert.IsNull(actualObject);
        }

        [Test]
        public void ShouldReturnNullForNullObject()
        {
            var actualObject = _converter.Convert(null, null, null, null);

            Assert.IsNull(actualObject);
        }

        [Test]
        public void ShouldReturnNullForNoEnum()
        {
            var test = 1;

            var actualObject = _converter.Convert(test, null, null, null);

            Assert.IsNull(actualObject);
        }
    }
}
