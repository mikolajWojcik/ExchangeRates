using ExchangeRates.Models.Enums;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Models
{
    public class CachedDataItem : BindableBase
    {
        private bool _isSelected;

        public DateTime Date { get; set; }

        public IEnumerable<CurrencyType> Types { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
    }
}
