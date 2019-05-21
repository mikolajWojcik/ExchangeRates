﻿using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Services.Interfaces
{
    public interface IChartsEntryAdapter
    {
        IEnumerable<Entry> CreateMicrochartsList(HistoricalRates rate, CurrencyType currencyType);
    }
}