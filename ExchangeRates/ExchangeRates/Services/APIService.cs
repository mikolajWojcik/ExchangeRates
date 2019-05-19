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

        public async Task<LatestRates> GetLatestAsync(CurrencyType? baseCurrency = null, IEnumerable<CurrencyType> symbols = null)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseURI);

                var query = CreateDefaultQuerry(baseCurrency, symbols, "latest");

                return await RunGetAsync<LatestRates>(httpClient, query.ToString());
            }
        }

        public async Task<HistoricalRates> GetHistoricalRatesAsync(DateTime startDate, DateTime endDate, CurrencyType? baseCurrency = null, IEnumerable<CurrencyType> symbols = null)
        {
            if (startDate < new DateTime(1999, 01, 01))
                throw new ArgumentException("Historical data is not supported before 1999-01-01");

            if (endDate > DateTime.Today)
                throw new ArgumentException("No future data is provided");

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseURI);

                var queryString = CreateDefaultQuerry(baseCurrency, symbols, "history");

                queryString = $"{queryString}start_at={FormatDateForQuery(startDate)}&";
                queryString = $"{queryString}end_at={FormatDateForQuery(endDate)}&";

                return await RunGetAsync<HistoricalRates>(httpClient, queryString);
            }
        }

        private static async Task<T> RunGetAsync<T>(HttpClient httpClient, string url) where T : new()
        {
            var response = await httpClient.GetAsync(url);

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
            var outputString = $"{_baseURI}{apiResource}?";

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
