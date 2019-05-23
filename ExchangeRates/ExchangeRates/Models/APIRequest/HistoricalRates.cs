using ExchangeRates.Models.Enums;
using ExchangeRates.Models.APIRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Microcharts;

namespace ExchangeRates.Models
{
    public class HistoricalRates : BaseRates
    {
        [JsonProperty(PropertyName = "rates")]
        public SortedDictionary<DateTime, Dictionary<CurrencyType, double>> Rates { get; set; }

        [JsonProperty(PropertyName = "start_at")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "end_at")]
        public DateTime EndDate { get; set; }
    }
}
