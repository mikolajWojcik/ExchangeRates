using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Services.Interfaces
{
    public interface IFilesManagerService
    {
        Dictionary<CurrencyType, Dictionary<int, IEnumerable<int>>> GetDataStoredOfflline(CurrencyType baseCurrency);

        IEnumerable<ExchangeRateItem> GetLatestOfflineData(CurrencyType baseCurrency, IEnumerable<CurrencyType> symbols);

        void SaveRates(HistoricalRates rates);
    }
}
