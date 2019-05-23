using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Services
{
    public class FilesManagerService : IFilesManagerService
    {
        public Task<SortedDictionary<DateTime, Dictionary<CurrencyType, double>>> GetDataStoredOffllineAsync(CurrencyType baseCurrency)
        {
            throw new NotImplementedException();
        }

        public Task SaveRatesAsync(SortedDictionary<DateTime, Dictionary<CurrencyType, double>> rates)
        {
            throw new NotImplementedException();
        }
    }
}
