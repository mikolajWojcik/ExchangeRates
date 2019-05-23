using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ExchangeRates.Services
{
    public class ChartsEntryAdapter : IChartsEntryAdapter
    {
        public IEnumerable<ChartEntry> CreateMicrochartsList(SortedDictionary<DateTime, Dictionary<CurrencyType, double>> rates, CurrencyType currencyType)
        {
            var outputList = new List<ChartEntry>();

            foreach(var rate in rates)
            {
                if(rate.Value.ContainsKey(currencyType))
                {
                    outputList.Add(new ChartEntry((float)(rate.Value[currencyType]))
                    {
                        ValueLabel = rate.Value[currencyType].ToString("f4"),
                        Label = rate.Key.Day.ToString()
                    });
                }
            }

            return outputList;
        }
    }
}
