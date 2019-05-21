using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using System;
using System.Collections.Generic;
using Entry = Microcharts.Entry;
using System.Threading.Tasks;

namespace ExchangeRates.Services.Interfaces
{
    public interface IExchangeRatesStore
    {
        Task<IEnumerable<ExchangeRateItem>> GetLatestRatesAsync();

        Task<IEnumerable<Entry>> GetDataForChartGenerationAsync(CurrencyType currencyType, int month, int year);
    }
}
