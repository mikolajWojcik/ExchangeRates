using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExchangeRates.ViewModels
{
	public class SettingsPageViewModel : ViewModelBase
	{
        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Application settings";
        }
    }
}
