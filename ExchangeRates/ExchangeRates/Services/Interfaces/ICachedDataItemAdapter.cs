using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Services.Interfaces
{
    public interface ICachedDataItemAdapter
    {
        IEnumerable<CachedDataItem> GetCachedData(SortedDictionary<DateTime, Dictionary<CurrencyType, double>> rates);

        SortedDictionary<DateTime, Dictionary<CurrencyType, double>> RemoveSelectedItemsFromRates(SortedDictionary<DateTime, Dictionary<CurrencyType, double>> rates, IEnumerable<CachedDataItem> cachedData);
    }
}
