using ExchangeRates.Models.Enums;
using ExchangeRates.Models.APIRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExchangeRates.Models
{
    public class LatestRates : BaseRates
    {
        [JsonProperty(PropertyName = "rates")]
        public Dictionary<CurrencyType, double> Rates { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        internal IEnumerable<ExchangeRateItem> ConvertToExchangeRateItem()
        {
            var returnList = new List<ExchangeRateItem>();

            foreach(var rate in Rates)
            {
                returnList.Add(new ExchangeRateItem
                {
                    BaseCurrencyType = Base,
                    CurrencyType = rate.Key,
                    Value = rate.Value,
                    Date = Date
                });
            }

            return returnList.OrderBy(x => x.CurrencyType.ToString());
        }
    }
}