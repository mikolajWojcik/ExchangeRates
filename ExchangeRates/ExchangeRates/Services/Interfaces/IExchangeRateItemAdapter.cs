using ExchangeRates.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Services.Interfaces
{
    public interface IExchangeRateItemAdapter
    {
        IEnumerable<ExchangeRateItem> CreateExchangeRateItemList(LatestRates latestRates);
    }
}
