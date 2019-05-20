using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Services.Interfaces
{
    public interface ISecureStorageWrapper
    {
        string BaseCurrencySettingLocation { get; }
        
        string SymbolsListSettingLocation { get; }

        Task<string> GetAsync(string key);

        Task SetAsync(string key, string value);
    }
}
