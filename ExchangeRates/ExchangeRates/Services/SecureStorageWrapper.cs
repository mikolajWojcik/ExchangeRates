using ExchangeRates.Services.Interfaces;
using System.Threading.Tasks;

namespace ExchangeRates.Services
{
    public class SecureStorageWrapper : ISecureStorageWrapper
    {
        public string BaseCurrencySettingLocation => "base-currency-type-location";

        public string SymbolsListSettingLocation => "symbols-list-location";

        public Task<string> GetAsync(string key)
        {
            return Xamarin.Essentials.SecureStorage.GetAsync(key);
        }

        public Task SetAsync(string key, string value)
        {
            return Xamarin.Essentials.SecureStorage.SetAsync(key, value);
        }
    }
}
