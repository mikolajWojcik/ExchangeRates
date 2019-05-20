using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ExchangeRates.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISecureStorageWrapper _wrapper;

        public SettingsService(ISecureStorageWrapper storageWrapper)
        {
            _wrapper = storageWrapper;
        }

        public async Task<CurrencyType> GetBaseCurrencyTypeAsync()
        {
            var returnObject = CurrencyType.EUR;

            try
            {
                var baseString = await _wrapper.GetAsync(_wrapper.BaseCurrencySettingLocation);

                if(!string.IsNullOrEmpty(baseString))
                    returnObject = (CurrencyType)Enum.Parse(typeof(CurrencyType), baseString);
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return returnObject;
        }

        public async Task<IEnumerable<CurrencyType>> GetSymbolsListAsync()
        {
            var returnList = new List<CurrencyType>
            {
                CurrencyType.GBP,
                CurrencyType.USD,
                CurrencyType.CHF
            };

            try
            {
                var listString = await _wrapper.GetAsync(_wrapper.SymbolsListSettingLocation);

                if(!string.IsNullOrEmpty(listString))
                    returnList = JsonConvert.DeserializeObject<List<CurrencyType>>(listString);
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return returnList;
        }

        public void RemoveAllSettings()
        {
            SecureStorage.RemoveAll();
        }

        public async Task SaveSettingsAsync(CurrencyType baseCurrency, IEnumerable<CurrencyType> symbols)
        {
            await SecureStorage.SetAsync(_wrapper.BaseCurrencySettingLocation, baseCurrency.ToString());

            var symbolsJson = JsonConvert.SerializeObject(symbols);
            await SecureStorage.SetAsync(_wrapper.SymbolsListSettingLocation, symbolsJson);
        }
    }
}
