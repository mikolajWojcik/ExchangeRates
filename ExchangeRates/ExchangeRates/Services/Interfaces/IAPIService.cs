using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Services.Interfaces
{
    public interface IAPIService
    {
        Task<LatestRates> GetLatestAsync(CurrencyType? baseCurrency = null, IEnumerable<CurrencyType> symbols = null);

        Task<HistoricalRates> GetHistoricalRatesAsync(DateTime startDate, DateTime endDate, CurrencyType? baseCurrency = null, IEnumerable<CurrencyType> symbols = null);
    }
}
