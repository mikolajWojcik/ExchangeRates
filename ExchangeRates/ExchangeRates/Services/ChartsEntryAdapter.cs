using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExchangeRates.Services
{
    public class ChartsEntryAdapter : IChartsEntryAdapter
    {
        public IEnumerable<Entry> CreateMicrochartsList(HistoricalRates rates, CurrencyType currencyType)
        {
            var outputList = new List<Entry>();

            foreach(var rate in rates.Rates)
            {
                if(rate.Value.ContainsKey(currencyType))
                {
                    outputList.Add(new Entry((float)(rate.Value[currencyType]))
                    {
                        Label = rate.Key.ToString("dd")
                    });
                }
            }

            return outputList.OrderBy(x => x.Label);
        }
    }
}
