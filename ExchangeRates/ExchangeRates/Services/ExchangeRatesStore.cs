using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using Entry = Microcharts.Entry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Services
{
    public class ExchangeRatesStore : IExchangeRatesStore
    {
        private readonly IAPIService _APIService;
        private readonly IFilesManagerService _filesManagerService;
        private readonly ISettingsService _settingsService;
        private readonly IChartsEntryAdapter _entryAdapter;

        public ExchangeRatesStore(IAPIService aPIService, IFilesManagerService filesManager, ISettingsService settingsService, IChartsEntryAdapter entryAdapter)
        {
            _APIService = aPIService;
            _filesManagerService = filesManager;
            _settingsService = settingsService;
            _entryAdapter = entryAdapter;
        }

        public async Task<IEnumerable<Entry>> GetDataForChartGenerationAsync(CurrencyType currencyType, int month, int year)
        {
            if (year < 1999)
                throw new ArgumentException("Data is not aviable for years before 1999");

            if (month < 1 || month > 12)
                throw new ArgumentException("Invaild month number");

            if (!_filesManagerService.IsDataAviableOffline(currencyType, month, year))
            {
                var baseCurrency = await _settingsService.LoadBaseCurrencyTypeAsync();

                var startDate = new DateTime(year, month, 1);
                var endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                var historicalRates = await _APIService.GetHistoricalRatesAsync(startDate, endDate, baseCurrency, new List<CurrencyType> { currencyType });
                return _entryAdapter.CreateMicrochartsList(historicalRates, currencyType);
            }
            else
            {
                //TO DO
                return new List<Entry>();
            }
        }

        public async Task<IEnumerable<ExchangeRateItem>> GetLatestRatesAsync()
        {
            var baseCurrency = await _settingsService.LoadBaseCurrencyTypeAsync();
            var symbolsList = await _settingsService.LoadSymbolsListAsync();

            var latestRates = await _APIService.GetLatestAsync(baseCurrency, symbolsList);

            return latestRates.ConvertToExchangeRateItem();
        }
    }
}
