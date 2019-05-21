using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using ExchangeRates.Views;
using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExchangeRates.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IExchangeRatesStore _ratesStore;
        private readonly ISettingsService _settingsService;
        private ObservableCollection<ExchangeRateItem> _rates;

        public MainPageViewModel(INavigationService navigationService, IExchangeRatesStore ratesStore, ISettingsService settingsService)
            : base(navigationService)
        {
            Title = "Main Page";

            _navigationService = navigationService;

            _ratesStore = ratesStore;

            _settingsService = settingsService;
            _settingsService.BaseCurrencyChanged += async (sender, e) => await RefreshRatesAsync();
            _settingsService.SymbolsListChanged += async (sender, e) => await RefreshRatesAsync();

            ShowSettingsCommand = new DelegateCommand(ShowSettings, () => !IsBusy)
                .ObservesProperty(() => IsBusy);

            ShowChartCommand = new DelegateCommand<ExchangeRateItem>(ShowChart, (t) => !IsBusy && t != null)
                .ObservesProperty(() => IsBusy);
            
        }

        public DelegateCommand RefreshCommand { get; }
        public DelegateCommand ShowSettingsCommand { get; }
        public DelegateCommand<ExchangeRateItem> ShowChartCommand { get; }

        public ObservableCollection<ExchangeRateItem> Rates
        {
            get { return _rates; }
            set { SetProperty(ref _rates, value); }
        }

        private async Task RefreshRatesAsync()
        {
            Rates = new ObservableCollection<ExchangeRateItem>(await _ratesStore.GetLatestRatesAsync());
        }

        private void ShowChart(ExchangeRateItem obj)
        {
            IsBusy = true;
            var parameters = new NavigationParameters
            {
                { nameof(ExchangeRateItem), obj }
            };

            NavigateToPage(nameof(ChartPage), parameters);
        }

        private void NavigateToPage(string pageName, NavigationParameters parameters = null)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await _navigationService.NavigateAsync(pageName, parameters)
                    .ContinueWith((t) =>
                    {
                        IsBusy = false;
                        return t.Result;
                    });

                if (!result.Success)
                {
                    Crashes.TrackError(result.Exception);
                }
            });
        }

        private void ShowSettings()
        {
            NavigateToPage(nameof(SettingsPage));
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            await Task.WhenAll(
                _settingsService.LoadBaseCurrencyTypeAsync(),
                _settingsService.LoadSymbolsListAsync()).ContinueWith(async(t) =>
                {
                    await RefreshRatesAsync();
                });
        }
    }
}
