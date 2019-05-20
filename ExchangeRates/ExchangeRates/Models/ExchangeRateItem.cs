using ExchangeRates.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Models
{
    public class ExchangeRateItem
    {
        public DateTime Date { get; set; }

        public CurrencyType CurrencyType { get; set; }

        public CurrencyType BaseCurrencyType { get; set; }

        public double Value { get; set; }
    }
}
