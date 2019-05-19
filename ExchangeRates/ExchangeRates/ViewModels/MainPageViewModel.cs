using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IAPIService _httpService;
        private bool _isBusy;
        private Dictionary<CurrencyType, double> _rates;

        public MainPageViewModel(INavigationService navigationService, IAPIService httpService)
            : base(navigationService)
        {
            Title = "Main Page";

            _httpService = httpService;

            RefreshCommand = new DelegateCommand(Refresh, CanRefresh)
                .ObservesProperty(() => IsBusy);
        }

        public DelegateCommand RefreshCommand { get; }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public Dictionary<CurrencyType, double> Rates
        {
            get { return _rates; }
            set { SetProperty(ref _rates, value); }
        }

        private bool CanRefresh()
        {
            return !IsBusy;
        }

        private async void Refresh()
        {
            IsBusy = true;

            var symbols = new List<CurrencyType>
            {
                CurrencyType.EUR,
                CurrencyType.CZK,
                CurrencyType.GBP,
                CurrencyType.PLN
            };
            var startDate = new DateTime(2018, 01, 01);
            var endDate = new DateTime(2018, 03, 01);

            Rates = await _httpService.GetLatestAsync(CurrencyType.EUR, symbols).ContinueWith((t) =>
            {
                IsBusy = false;
                return t.Result.Rates;
            });
        }
    }
}
