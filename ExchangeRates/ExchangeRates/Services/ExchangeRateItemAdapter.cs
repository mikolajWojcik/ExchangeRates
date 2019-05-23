using ExchangeRates.Models;
using ExchangeRates.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExchangeRates.Services
{
    public class ExchangeRateItemAdapter : IExchangeRateItemAdapter
    {
        public IEnumerable<ExchangeRateItem> CreateExchangeRateItemList(LatestRates latestRates)
        {
            var returnList = new List<ExchangeRateItem>();

            foreach (var rate in latestRates.Rates)
            {
                returnList.Add(new ExchangeRateItem
                {
                    BaseCurrencyType = latestRates.Base,
                    CurrencyType = rate.Key,
                    Value = rate.Value,
                    Date = latestRates.Date,
                    ChartDate = latestRates.Date
                });
            }

            return returnList.OrderBy(x => x.CurrencyType.ToString());
        }
    }
}
