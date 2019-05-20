using ExchangeRates.Models.Enums;
using ExchangeRates.Services;
using ExchangeRates.Services.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Services
{   
    public class APIServiceTest
    {
        private readonly IAPIService _service;

        public APIServiceTest()
        {
            _service = new APIService();
        }

        [Test]
        public async Task ShouldGetLatesRatesWithoutParameters()
        {
            var response = await _service.GetLatestAsync();
            Assert.IsNotNull(response);
        }

        [Test]
        public async Task ShouldGetLatesRatesForBase()
        {
            var baseCurrency = CurrencyType.PLN;
            var response = await _service.GetLatestAsync(baseCurrency);

            Assert.AreEqual(baseCurrency, response.Base);
        }

        [Test]
        public async Task ShouldGetLatestRatesForListOfSymbols()
        {
            var baseCurrency = CurrencyType.PLN;
            var symbols = new List<CurrencyType>
            {
                CurrencyType.EUR,
                CurrencyType.CZK
            };
            var response = await _service.GetLatestAsync(baseCurrency, symbols);

            Assert.AreEqual(2, response.Rates.Count);
            Assert.That(response.Rates.Any(x => x.Key == CurrencyType.EUR));
            Assert.That(response.Rates.Any(x => x.Key == CurrencyType.CZK));
        }
        
        [Test]
        public async Task ShouldGetLatestRatesForWhenSymbolsContainBase()
        {
            var baseCurrency = CurrencyType.CZK;
            var symbols = new List<CurrencyType>
            {
                CurrencyType.EUR,
                CurrencyType.CZK
            };
            var response = await _service.GetLatestAsync(baseCurrency, symbols);

            Assert.AreEqual(1, response.Rates.Count);
            Assert.That(response.Rates.Any(x => x.Key == CurrencyType.EUR));
        }

        [Test]
        public async Task ShouldGetHistoricalWithoutParamters()
        {
            var startDate = new DateTime(2019, 05, 13);
            var endDate = new DateTime(2019, 05, 17);
            var response = await _service.GetHistoricalRatesAsync(startDate, endDate);

            Assert.AreEqual(5, response.Rates.Count);
        }

        [Test]
        public async Task ShouldGetHistoricalForBase()
        {
            var startDate = new DateTime(2019, 05, 13);
            var endDate = new DateTime(2019, 05, 17);
            var baseCurrency = CurrencyType.PLN;
            var response = await _service.GetHistoricalRatesAsync(startDate, endDate, baseCurrency);

            Assert.AreEqual(baseCurrency, response.Base);
        }

        [Test]
        public async Task ShouldGetHistoricalForListOfSymbols()
        {
            var startDate = new DateTime(2019, 05, 13);
            var endDate = new DateTime(2019, 05, 17);
            var baseCurrency = CurrencyType.PLN;
            var symbols = new List<CurrencyType>
            {
                CurrencyType.EUR,
                CurrencyType.CZK
            };
            var response = await _service.GetHistoricalRatesAsync(startDate, endDate, baseCurrency, symbols);

            Assert.That(response.Rates.All(x => x.Value.Count == 2));
        }

        [Test]
        public void ShouldThrowWhenStartDateToEarly()
        {
            var startDate = new DateTime(1998, 05, 13);
            var endDate = new DateTime(2019, 05, 17);

            Assert.ThrowsAsync(typeof(ArgumentException), 
                async () => await _service.GetHistoricalRatesAsync(startDate, endDate));
        }

        [Test]
        public void ShouldThrowWhenEndDateBeforStartDate()
        {
            var startDate = new DateTime(2019, 05, 13);
            var endDate = new DateTime(2019, 04, 17);

            Assert.ThrowsAsync(typeof(ArgumentException), 
                async () => await _service.GetHistoricalRatesAsync(startDate, endDate));
        }
    }
}
