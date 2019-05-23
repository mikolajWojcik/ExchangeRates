using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Services.Interfaces
{
    public interface IFilesManagerService
    {
        Task<SortedDictionary<DateTime, Dictionary<CurrencyType, double>>> GetDataStoredOffllineAsync(CurrencyType baseCurrency);

        Task SaveRatesAsync(CurrencyType baseCurrency, SortedDictionary<DateTime, Dictionary<CurrencyType, double>> rates);
    }
}
