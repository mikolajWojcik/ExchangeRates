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

    }
}