using ExchangeRates.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Services.Interfaces
{
    public interface ISettingsService
    {
        CurrencyType GetBaseCurrencyType();

        IEnumerable<CurrencyType> GetSymbolsList();
    }
}
