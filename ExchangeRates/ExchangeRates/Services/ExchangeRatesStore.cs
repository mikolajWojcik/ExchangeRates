using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Services
{
    public class ExchangeRatesStore : IExchangeRatesStore
    {
        private readonly IAPIService _APIService;
        private readonly IFilesManagerService _filesManagerService;
        private readonly ISettingsService _settingsService;

        public ExchangeRatesStore(IAPIService aPIService, IFilesManagerService filesManager, ISettingsService settingsService)
        {
            _APIService = aPIService;
            _filesManagerService = filesManager;
            _settingsService = settingsService;
        }
        public IEnumerable<ExchangeRateItem> GetDataForChartGeneration(CurrencyType currencyType, int month, int year)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExchangeRateItem> GetLatestRates()
        {
            throw new NotImplementedException();
        }
    }
}
