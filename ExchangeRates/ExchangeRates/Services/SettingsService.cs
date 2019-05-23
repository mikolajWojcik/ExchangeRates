using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using ExchangeRates.Services.Interfaces.Base;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ExchangeRates.Services
{
    public class SettingsService : BindableBase, ISettingsService
    {      
        private CurrencyType _baseCurrency;
        private IEnumerable<CurrencyType> _symbolsList = new List<CurrencyType>
        {
            CurrencyType.GBP,
            CurrencyType.USD,
            CurrencyType.CHF
        };
        private readonly ISecureStorageWrapper _wrapper;

        public SettingsService(ISecureStorageWrapper storageWrapper)
        {
            _wrapper = storageWrapper;

            Initialize = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await LoadBaseCurrencyTypeAsync();
            await LoadSymbolsListAsync();
        }

        public CurrencyType BaseCurrency
        {
            get { return _baseCurrency; }
            set { SetProperty(ref _baseCurrency, value, () => BaseCurrencyChanged?.Invoke(this, new EventArgs())); }
        }

        public IEnumerable<CurrencyType> SymbolsList
        {
            get { return _symbolsList; }
            set { SetProperty(ref _symbolsList, value, () => SymbolsListChanged?.Invoke(this, new EventArgs())); }
        }

        public Task Initialize { get; }

        public event EventHandler BaseCurrencyChanged;
        public event EventHandler SymbolsListChanged;

        public async Task<CurrencyType> LoadBaseCurrencyTypeAsync()
        {
            try
            {
                var baseString = await _wrapper.GetAsync(_wrapper.BaseCurrencySettingLocation);

                if (!string.IsNullOrEmpty(baseString))
                    BaseCurrency = (CurrencyType)Enum.Parse(typeof(CurrencyType), baseString);             
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return BaseCurrency;
        }

        public async Task<IEnumerable<CurrencyType>> LoadSymbolsListAsync()
        {
            try
            {
                var listString = await _wrapper.GetAsync(_wrapper.SymbolsListSettingLocation);

                if(!string.IsNullOrEmpty(listString))
                    SymbolsList = JsonConvert.DeserializeObject<List<CurrencyType>>(listString);
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return SymbolsList;
        }

        public async Task SaveSettingsAsync()
        {
            await SecureStorage.SetAsync(_wrapper.BaseCurrencySettingLocation, BaseCurrency.ToString());

            var symbolsJson = JsonConvert.SerializeObject(SymbolsList);
            await SecureStorage.SetAsync(_wrapper.SymbolsListSettingLocation, symbolsJson);
        }
    }
}
