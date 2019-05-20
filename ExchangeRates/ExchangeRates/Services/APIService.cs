using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ExchangeRates.Services
{
    public class APIService : IAPIService
    {
        private readonly string _baseURI = "https://api.exchangeratesapi.io/";
        private readonly HttpClient _httpClient;

        public APIService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseURI)
            };
        }

        public async Task<LatestRates> GetLatestAsync(CurrencyType? baseCurrency = null, IEnumerable<CurrencyType> symbols = null)
        {
            var query = CreateDefaultQuerry(baseCurrency, symbols, "latest");

            return await RunGetAsync<LatestRates>(query);            
        }

        public async Task<HistoricalRates> GetHistoricalRatesAsync(DateTime startDate, DateTime endDate, CurrencyType? baseCurrency = null, IEnumerable<CurrencyType> symbols = null)
        {
            if (startDate < new DateTime(1999, 01, 01))
                throw new ArgumentException("Historical data is not supported before 1999-01-01");

            if (endDate < startDate)
                throw new ArgumentException("End date should be after start date");

            var queryString = CreateDefaultQuerry(baseCurrency, symbols, "history");

            queryString = $"{queryString}start_at={FormatDateForQuery(startDate)}&";
            queryString = $"{queryString}end_at={FormatDateForQuery(endDate)}&";

            return await RunGetAsync<HistoricalRates>(queryString);            
        }

        private async Task<T> RunGetAsync<T>(string url) where T : new()
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(stringContent);
            }
            else
            {
                return default(T);
            }
        }

        private string CreateDefaultQuerry(CurrencyType? baseCurrency, IEnumerable<CurrencyType> symbols, string apiResource)
        {
            var outputString = $"{apiResource}?";

            if (baseCurrency != null)
                outputString = $"{outputString}base={baseCurrency}&";

            if (symbols != null)
            {
                var symbolsQueryBuilder = new StringBuilder();

                foreach (var symbol in symbols)
                {
                    if (baseCurrency.HasValue && !baseCurrency.Value.Equals(symbol))
                    {
                        symbolsQueryBuilder.Append(symbol.ToString());
                        symbolsQueryBuilder.Append(",");
                    }
                }
                symbolsQueryBuilder.Remove(symbolsQueryBuilder.Length - 1, 1);
                outputString = $"{outputString}symbols={symbolsQueryBuilder.ToString()}&";
            }
            return outputString;
        }

        private string FormatDateForQuery(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
