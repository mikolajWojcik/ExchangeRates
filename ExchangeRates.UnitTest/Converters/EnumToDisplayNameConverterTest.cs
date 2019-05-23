﻿using ExchangeRates.Converters;
using ExchangeRates.Helpers;
using ExchangeRates.Models.Attributes;
using ExchangeRates.Models.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Test.Converters
{
    public class EnumToDisplayNameConverterTest
    {
        private readonly EnumToAttributeConverter _converter;

        public EnumToDisplayNameConverterTest()
        {
            _converter = new EnumToAttributeConverter(); 
        }

        [Test]
        public void ShouldReturnDisplayNameAttribute()
        {
            var testEnum = CurrencyType.CZK;
            var displayName = testEnum.GetAttribute<EnumHelperAttribute>().CurrencyName;

            var actualDisplayName = _converter.Convert(testEnum, null, null, null);

            Assert.AreEqual(displayName, actualDisplayName);
        }

        [Test]
        public void ShouldReturnToStringWhenNoAttribute()
        {
            var testEnum = DayOfWeek.Monday;

            var actualDisplayName = _converter.Convert(testEnum, null, null, null);

            Assert.AreEqual(testEnum.ToString(), actualDisplayName);
        }

        [Test]
        public void ShouldReturnEmptyStringWhenNoEnum()
        {
            var testObject = new { Value = 123 };

            var actualDisplayName = _converter.Convert(testObject, null, null, null);

            Assert.IsEmpty(actualDisplayName.ToString());
        }
    }
}
