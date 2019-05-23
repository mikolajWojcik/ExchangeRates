using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microcharts;
using System;
using ExchangeRates.Services.Interfaces.Base;

namespace ExchangeRates.Services.Interfaces
{
    public interface IExchangeRatesStore : IAsyncInitialization
    {
        Task<IEnumerable<ExchangeRateItem>> GetLatestRatesAsync();

        Task<IEnumerable<ChartEntry>> GetDataForChartGenerationAsync(CurrencyType currencyType, int month, int year);

        SortedDictionary<DateTime, Dictionary<CurrencyType, double>> Rates { get; set; }
    }
}
