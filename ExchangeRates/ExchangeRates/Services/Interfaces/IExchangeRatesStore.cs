using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Services.Interfaces
{
    public interface IExchangeRatesStore
    {
        IEnumerable<ExchangeRateItem> GetLatestRates();

        IEnumerable<ExchangeRateItem> GetDataForChartGeneration(CurrencyType currencyType, int month, int year);
    }
}
