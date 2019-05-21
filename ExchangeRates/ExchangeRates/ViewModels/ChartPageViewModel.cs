using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExchangeRates.ViewModels
{
	public class ChartPageViewModel : ViewModelBase
	{
        public ChartPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Chart view";
        }
	}
}
