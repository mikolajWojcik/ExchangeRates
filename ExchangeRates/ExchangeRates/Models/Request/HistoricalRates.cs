using ExchangeRates.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Models
{
    public class HistoricalRates
    {
        [JsonProperty(PropertyName = "rates")]
        public Dictionary<DateTime, Dictionary<CurrencyType, double>> Rates { get; set; }

        [JsonProperty(PropertyName = "start_at")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "end_at")]
        public DateTime EndDate { get; set; }
    }
}
