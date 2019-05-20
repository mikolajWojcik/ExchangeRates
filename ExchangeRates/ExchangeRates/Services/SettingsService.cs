using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Services
{
    public class SettingsService : ISettingsService
    {
        public CurrencyType GetBaseCurrencyType()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CurrencyType> GetSymbolsList()
        {
            throw new NotImplementedException();
        }
    }
}
