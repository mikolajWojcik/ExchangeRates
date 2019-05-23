using ExchangeRates.Models.Enums;
using Microcharts;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Models
{
    public class ExchangeRateItem : BindableBase
    {
        private bool _isChartVisible;
        private DateTime _chartDate;
        private IEnumerable<ChartEntry> _chartEntries;

        public event EventHandler ChartDateChanged;

        public DateTime Date { get; set; }

        public CurrencyType CurrencyType { get; set; }

        public CurrencyType BaseCurrencyType { get; set; }

        public double Value { get; set; }

        public IEnumerable<ChartEntry> ChartEntries
        {
            get { return _chartEntries; }
            set { SetProperty(ref _chartEntries, value); }
        }

        public bool IsChartVisible
        {
            get { return _isChartVisible; }
            set { SetProperty(ref _isChartVisible, value); }
        }

        public DateTime ChartDate
        {
            get { return _chartDate; }
            set { SetProperty(ref _chartDate, value, HandleChartDateChanged); }
        }

        private void HandleChartDateChanged()
        {
            ChartDateChanged?.Invoke(this, new EventArgs());
        }
    }
}
