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
        private ObservableCollection<ExchangeRateItem> _rates;

        public MainPageViewModel(INavigationService navigationService, IExchangeRatesStore ratesStore, ISettingsService settingsService)
            : base(navigationService)
        {
            Title = "Exchange rates application";

            _navigationService = navigationService;

            _ratesStore = ratesStore;

            SettingsService = settingsService;
            SettingsService.BaseCurrencyChanged += async (sender, e) => await RefreshRatesAsync();
            SettingsService.SymbolsListChanged += async (sender, e) => await RefreshRatesAsync();

            ShowSettingsCommand = new DelegateCommand(ShowSettings, () => !IsBusy)
                .ObservesProperty(() => IsBusy);

            ShowChartCommand = new DelegateCommand<ExchangeRateItem>(ShowChart, (t) => !IsBusy && t != null)
                .ObservesProperty(() => IsBusy);

            ShowCachedDataCommand = new DelegateCommand(ShowCachedData, () => !IsBusy)
                .ObservesProperty(() => IsBusy);

            DecrementChartDateCommand = new DelegateCommand<ExchangeRateItem>(DecrementChartDate, CanDecrementDate)
                .ObservesProperty(() => IsBusy);

            IncrementChartDateCommand = new DelegateCommand<ExchangeRateItem>(IncrementChartDate, CanIncrementChartDate)
                .ObservesProperty(() => IsBusy);
        }

        public ISettingsService SettingsService { get; }
        public DelegateCommand RefreshCommand { get; }
        public DelegateCommand ShowSettingsCommand { get; }
        public DelegateCommand<ExchangeRateItem> ShowChartCommand { get; }
        public DelegateCommand ShowCachedDataCommand { get; }
        public DelegateCommand<ExchangeRateItem> DecrementChartDateCommand { get; }
        public DelegateCommand<ExchangeRateItem> IncrementChartDateCommand { get; }

        public ObservableCollection<ExchangeRateItem> Rates
        {
            get { return _rates; }
            set { SetProperty(ref _rates, value); }
        }

        private void ShowCachedData()
        {
            NavigateToPage(nameof(DataManagerPage));
        }

        private void IncrementChartDate(ExchangeRateItem obj)
        {
            obj.ChartDate = obj.ChartDate.AddMonths(1);
        }

        private bool CanIncrementChartDate(ExchangeRateItem arg)
        {
            return !IsBusy && arg != null && arg.ChartDate.AddMonths(1) <= DateTime.Now;
        }

        private void DecrementChartDate(ExchangeRateItem obj)
        {
            obj.ChartDate = obj.ChartDate.AddMonths(-1);
        }

        private bool CanDecrementDate(ExchangeRateItem arg)
        {
            return !IsBusy && arg != null && arg.ChartDate.AddMonths(-1) >= new DateTime(1999, 1, 1);
        }

        private async Task RefreshRatesAsync()
        {
            Rates = new ObservableCollection<ExchangeRateItem>(await _ratesStore.GetLatestRatesAsync());

            foreach(var rate in Rates)
            {
                rate.ChartDateChanged += OnItemChartDateChanged;
            }
        }

        private async void ShowChart(ExchangeRateItem obj)
        {
            obj.IsChartVisible = !obj.IsChartVisible;

            if(obj.IsChartVisible)
            {
                obj.ChartEntries = await _ratesStore.GetDataForChartGenerationAsync(obj.CurrencyType, obj.ChartDate.Month, obj.ChartDate.Year);
            }
        }

        private async void OnItemChartDateChanged(object sender, EventArgs e)
        {
            var item = (ExchangeRateItem)sender;

            if(item.IsChartVisible)
                item.ChartEntries = await _ratesStore.GetDataForChartGenerationAsync(item.CurrencyType, item.ChartDate.Month, item.ChartDate.Year);

            IncrementChartDateCommand.RaiseCanExecuteChanged();
            DecrementChartDateCommand.RaiseCanExecuteChanged();
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
            await _ratesStore.Initialize.ContinueWith(async(t) =>
            {
                await RefreshRatesAsync();
            });
        }
    }
}
