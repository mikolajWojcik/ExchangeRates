using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Services
{
    public class FilesManagerService : IFilesManagerService
    {
        public Dictionary<CurrencyType, Dictionary<int, IEnumerable<int>>> GetDataStoredOfflline(CurrencyType baseCurrency)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExchangeRateItem> GetLatestOfflineData(CurrencyType baseCurrency, IEnumerable<CurrencyType> symbols)
        {
            throw new NotImplementedException();
        }
    }
}
