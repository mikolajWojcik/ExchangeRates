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
using ExchangeRates.Services.Interfaces.Base;

namespace ExchangeRates.Services
{
    public class ExchangeRatesStore : IExchangeRatesStore
    {
        private readonly IAPIService _APIService;
        private readonly IFilesManagerService _filesManagerService;
        private readonly ISettingsService _settingsService;
        private readonly IChartsEntryAdapter _entryAdapter;
        private readonly IExchangeRateItemAdapter _rateItemAdapter;

        public ExchangeRatesStore(IAPIService aPIService, IFilesManagerService filesManager, ISettingsService settingsService, IChartsEntryAdapter entryAdapter, IExchangeRateItemAdapter rateItemAdapter)
        {
            _APIService = aPIService;
            _filesManagerService = filesManager;
            _settingsService = settingsService;
            _entryAdapter = entryAdapter;
            _rateItemAdapter = rateItemAdapter;

            Initialize = InitializeAsync();
        }

        public SortedDictionary<DateTime, Dictionary<CurrencyType, double>> Rates { get; set; }

        public Task Initialize { get; }

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
                var historicalRates = await _APIService.GetHistoricalRatesAsync(startDate, endDate, _settingsService.BaseCurrency, _settingsService.SymbolsList);
                await UpdateRatesAsync(historicalRates);

                return _entryAdapter.CreateChartEntryList(historicalRates.Rates, currencyType);
            }
            else
            {
                var filteredRates = GetFilteredRates(startDate, endDate);
                return _entryAdapter.CreateChartEntryList(filteredRates, currencyType);
            }
        }

        public async Task<IEnumerable<ExchangeRateItem>> GetLatestRatesAsync()
        {
            var latestRates = await _APIService.GetLatestAsync(_settingsService.BaseCurrency, _settingsService.SymbolsList);

            return _rateItemAdapter.CreateExchangeRateItemList(latestRates);
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
            return Rates != null &&
                Rates.Count(x => x.Key >= startDate && x.Key <= endDate && x.Value.ContainsKey(currencyType)) > 0;
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

            await _filesManagerService.SaveRatesAsync(_settingsService.BaseCurrency, Rates);
        }

        private async Task InitializeAsync()
        {
            await _settingsService.Initialize.ContinueWith(async (t) =>
            {
                Rates = await _filesManagerService.GetDataStoredOffllineAsync(_settingsService.BaseCurrency);
            });
        }
    }
}
