using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExchangeRates.Services
{
    public class CachedDataItemAdapter : ICachedDataItemAdapter
    {
        public IEnumerable<CachedDataItem> GetCachedData(SortedDictionary<DateTime, Dictionary<CurrencyType, double>> rates)
        {
            var returnList = new List<CachedDataItem>();

            if (rates == null)
                return returnList;

            foreach(var rate in rates)
            {
                if(!returnList.Any(x => x.Date.Month == rate.Key.Month && x.Date.Year == rate.Key.Year))
                {
                    returnList.Add(new CachedDataItem
                    {
                        Date = new DateTime(rate.Key.Year, rate.Key.Month, 1),
                        Types = rate.Value.Keys.AsEnumerable()
                    });
                }                
            }

            return returnList;
        }

        public SortedDictionary<DateTime, Dictionary<CurrencyType, double>> RemoveSelectedItemsFromRates(SortedDictionary<DateTime, Dictionary<CurrencyType, double>>  rates, IEnumerable<CachedDataItem> cachedData)
        {
            foreach(var item in cachedData)
            {
                if(item.IsSelected)
                {
                    var itemYear = item.Date.Year;
                    var itemMonth = item.Date.Month;
                    var date = new DateTime(itemYear, itemMonth, 1);
                    var endDate = new DateTime(itemYear, item.Date.Month, DateTime.DaysInMonth(itemYear, itemMonth));

                    while(date <= endDate)
                    {
                        if (rates.ContainsKey(date))
                            rates.Remove(date);

                        date = date.AddDays(1);

                        Console.WriteLine(date);
                    }
                }
            }

            return rates;
        }
    }
}
