using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microcharts;

namespace ExchangeRates.Services.Interfaces
{
    public interface IExchangeRatesStore
    {
        Task<IEnumerable<ExchangeRateItem>> GetLatestRatesAsync();

        Task<IEnumerable<ChartEntry>> GetDataForChartGenerationAsync(CurrencyType currencyType, int month, int year);
    }
}
