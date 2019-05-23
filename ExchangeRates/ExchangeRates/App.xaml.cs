using Prism;
using Prism.Ioc;
using ExchangeRates.ViewModels;
using ExchangeRates.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExchangeRates.Services.Interfaces;
using ExchangeRates.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ExchangeRates
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterViewsForNavigation(containerRegistry);
            RegisterServices(containerRegistry);
        }

        private static void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAPIService, APIService>();
            containerRegistry.RegisterSingleton<ISettingsService, SettingsService>();
            containerRegistry.RegisterSingleton<IFilesManagerService, FilesManagerService>();
            containerRegistry.RegisterSingleton<IExchangeRatesStore, ExchangeRatesStore>();

            containerRegistry.RegisterSingleton<ISecureStorageWrapper, SecureStorageWrapper>();

            containerRegistry.RegisterSingleton<IChartsEntryAdapter, ChartsEntryAdapter>();
            containerRegistry.RegisterSingleton<IExchangeRateItemAdapter, ExchangeRateItemAdapter>();
        }

        private static void RegisterViewsForNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ChartPage, ChartPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
        }
    }
}
