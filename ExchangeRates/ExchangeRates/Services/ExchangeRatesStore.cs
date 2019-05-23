using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using Entry = Microcharts.Entry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using System.Linq;

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

        public SortedDictionary<DateTime, Dictionary<CurrencyType, double>> Rates { get; set; }

        public async Task<IEnumerable<ChartEntry>> GetDataForChartGenerationAsync(CurrencyType currencyType, int month, int year)
        {
            if (year < 1999)
                throw new ArgumentException("Data is not aviable for years before 1999");

            if (month < 1 || month > 12)
                throw new ArgumentException("Invaild month number");

            var startDate = new DateTime(year, month, 1);
            var endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            if (!IsDataAviableOffline(currencyType, startDate, endDate))
            {
                var baseCurrency = await _settingsService.LoadBaseCurrencyTypeAsync();
                var symbolsList = await _settingsService.LoadSymbolsListAsync();             

                var historicalRates = await _APIService.GetHistoricalRatesAsync(startDate, endDate, baseCurrency, symbolsList);
                await UpdateRatesAsync(historicalRates);

                return _entryAdapter.CreateMicrochartsList(historicalRates.Rates, currencyType);
            }
            else
            {
                var filteredRates = GetFilteredRates(startDate, endDate);
                return _entryAdapter.CreateMicrochartsList(filteredRates, currencyType);
            }
        }

        public async Task<IEnumerable<ExchangeRateItem>> GetLatestRatesAsync()
        {
            var baseCurrency = await _settingsService.LoadBaseCurrencyTypeAsync();
            var symbolsList = await _settingsService.LoadSymbolsListAsync();

            var latestRates = await _APIService.GetLatestAsync(baseCurrency, symbolsList);

            return latestRates.ConvertToExchangeRateItem();
        }

        public async Task InitializeAsync()
        {
            var baseCurrency = await _settingsService.LoadBaseCurrencyTypeAsync();
            Rates = await _filesManagerService.GetDataStoredOffllineAsync(baseCurrency);
        }

        private SortedDictionary<DateTime, Dictionary<CurrencyType, double>> GetFilteredRates( DateTime startDate, DateTime endDate)
        {
            var returnDictionary = new SortedDictionary<DateTime, Dictionary<CurrencyType, double>>();
            var values = Rates.Where(x => x.Key >= startDate && x.Key <= endDate);

            foreach(var value in values)
            {
                returnDictionary.Add(value.Key, value.Value);
            }

            return returnDictionary;
        }

        private bool IsDataAviableOffline(CurrencyType currencyType, DateTime startDate, DateTime endDate)
        {
            return Rates.Count(x => x.Key >= startDate && x.Key <= endDate && x.Value.ContainsKey(currencyType)) > 0;
        }

        private async Task UpdateRatesAsync(HistoricalRates historicalRates)
        {
            foreach(var rate in historicalRates.Rates)
            {
                if(!Rates.ContainsKey(rate.Key))
                {
                    Rates.Add(rate.Key, rate.Value);
                }
                else if(Rates[rate.Key] != rate.Value)
                {
                    Rates[rate.Key] = rate.Value;
                }
            }

            await _filesManagerService.SaveRatesAsync(Rates);
        }
    }
}
