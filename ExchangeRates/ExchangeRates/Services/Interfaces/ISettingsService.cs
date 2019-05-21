using ExchangeRates.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<CurrencyType> LoadBaseCurrencyTypeAsync();
        Task<IEnumerable<CurrencyType>> LoadSymbolsListAsync();
        Task SaveSettingsAsync();

        CurrencyType BaseCurrency { get; set; }
        IEnumerable<CurrencyType> SymbolsList { get; set; }

        event EventHandler BaseCurrencyChanged;
        event EventHandler SymbolsListChanged;
    }
}
