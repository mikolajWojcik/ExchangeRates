using ExchangeRates.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<CurrencyType> GetBaseCurrencyTypeAsync();

        Task<IEnumerable<CurrencyType>> GetSymbolsListAsync();

        Task SaveSettingsAsync(CurrencyType baseCurrency, IEnumerable<CurrencyType> symbols);
    }
}
