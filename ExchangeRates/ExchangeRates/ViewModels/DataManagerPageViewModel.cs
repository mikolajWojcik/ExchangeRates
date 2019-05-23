using ExchangeRates.Models;
using ExchangeRates.Services;
using ExchangeRates.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExchangeRates.ViewModels
{
	public class DataManagerPageViewModel : ViewModelBase
	{
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IExchangeRatesStore _exchangeRatesStore;
        private readonly ICachedDataItemAdapter _itemAdapter;
        private readonly IFilesManagerService _filesManager;
        private readonly ISettingsService _settingsService;
        private IEnumerable<CachedDataItem> _items;

        public DataManagerPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,  IExchangeRatesStore exchangeRatesStore, ICachedDataItemAdapter itemAdapter, IFilesManagerService filesManager, ISettingsService settingsService) 
            : base(navigationService)
        {
            Title = "Cached data manager";

            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _exchangeRatesStore = exchangeRatesStore;
            _itemAdapter = itemAdapter;
            _filesManager = filesManager;
            _settingsService = settingsService;

            SwitchToggledCommand = new DelegateCommand<object>(SwitchToggled);
            RemoveSelectedCommand = new DelegateCommand(RemoveSelected, CanRemoveSelected);
        }

        private void SwitchToggled(object obj)
        {
            if (obj is bool toggled)
            {
                if (!toggled)
                {
                    foreach (var item in Items)
                        item.IsSelected = true;
                }
                else
                {
                    foreach (var item in Items)
                        item.IsSelected = false;
                }
            }
        }

        public DelegateCommand<object> SwitchToggledCommand { get; }
        public DelegateCommand RemoveSelectedCommand { get; }

        public IEnumerable<CachedDataItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Items = _itemAdapter.GetCachedData(_exchangeRatesStore.Rates);
            
            foreach(var item in Items)
                item.PropertyChanged += OnItemPropertyChanged;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            foreach (var item in Items)
                item.PropertyChanged -= OnItemPropertyChanged;
        }

        private void OnItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RemoveSelectedCommand.RaiseCanExecuteChanged();
        }

        private async void RemoveSelected()
        {
            var count = Items.Count(x => x.IsSelected);
            var title = "Warning!";
            var message = $"Are you sure, that you want to delete {count} items?";

            var result = await BeginInvokeOnMainThreadAsync(() => _pageDialogService.DisplayAlertAsync(title, message, "Yes", "No"));

            if(result)
            {
                await BeginInvokeOnMainThreadAsync(() => _navigationService.GoBackAsync());

                var updatedRates = _itemAdapter.RemoveSelectedItemsFromRates(_exchangeRatesStore.Rates, Items);
                await _filesManager.SaveRatesAsync(_settingsService.BaseCurrency, updatedRates);
            }
        }

        private bool CanRemoveSelected()
        {
            return Items != null && Items.Count(x => x.IsSelected) > 0;
        }

        private Task<T> BeginInvokeOnMainThreadAsync<T>(Func<Task<T>> a)
        {
            var tcs = new TaskCompletionSource<T>();
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var result = await a();
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }

        private Task<bool> BeginInvokeOnMainThreadAsync(Func<Task> a)
        {
            var tcs = new TaskCompletionSource<bool>();
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    await a();
                    tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }
    }
}
