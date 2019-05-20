using ExchangeRates.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Models.APIRequest
{
    public class BaseRates
    {
        [JsonProperty(PropertyName = "base")]
        public CurrencyType Base { get; set; }

    }
}
