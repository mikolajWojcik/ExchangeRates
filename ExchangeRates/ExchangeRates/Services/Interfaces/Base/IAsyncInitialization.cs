using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Services.Interfaces.Base
{
    public interface IAsyncInitialization
    {
        Task Initialize { get; }
    }
}
