using ExchangeRates.Models.Enums;
using ExchangeRates.Services;
using ExchangeRates.Services.Interfaces;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Services
{
    public class SettingsServiceTest
    {
        [Test]
        public async Task ShouldGetDefaultBaseCurrency()
        {
            var mock = new Mock<ISecureStorageWrapper>();
            mock.SetReturnsDefault<string>(string.Empty);
            ISettingsService service = new SettingsService(mock.Object);

            var baseCurrency = await service.LoadBaseCurrencyTypeAsync();

            Assert.AreEqual(CurrencyType.EUR, baseCurrency);
        }

        [Test]
        public async Task ShouldGetDefaultSymbolsList()
        {
            var mock = new Mock<ISecureStorageWrapper>();
            mock.SetReturnsDefault<string>(string.Empty);
            ISettingsService service = new SettingsService(mock.Object);

            var symbolsList = await service.LoadSymbolsListAsync();
            var defaultList = new List<CurrencyType>
            {
                CurrencyType.GBP,
                CurrencyType.USD,
                CurrencyType.CHF
            };

            Assert.That(symbolsList, Is.EquivalentTo(defaultList));
        }

        [Test]
        public async Task ShouldGetSavedBaseCurrencyType()
        {
            var baseCurrency = CurrencyType.CZK;
            var mock = new Mock<ISecureStorageWrapper>();
            mock.Setup(x => x.GetAsync(mock.Object.BaseCurrencySettingLocation))
                .Returns(Task.FromResult<string>(baseCurrency.ToString()));
            ISettingsService service = new SettingsService(mock.Object);

            var actualCurrency = await service.LoadBaseCurrencyTypeAsync();

            Assert.AreEqual(baseCurrency, actualCurrency);
        }

        [Test]
        public async Task ShouldGetSavedSymbolsList()
        {
            IEnumerable<CurrencyType> symbolsList = new List<CurrencyType>
            {
                CurrencyType.CZK,
                CurrencyType.CAD,
                CurrencyType.AUD
            };
            var mock = new Mock<ISecureStorageWrapper>();
            mock.Setup(x => x.GetAsync(mock.Object.SymbolsListSettingLocation))
                .Returns(Task.FromResult<string>(JsonConvert.SerializeObject(symbolsList)));
            ISettingsService service = new SettingsService(mock.Object);

            var actualSymbolsList = await service.LoadSymbolsListAsync();

            Assert.That(actualSymbolsList, Is.EquivalentTo(symbolsList));
        }
    }
}
