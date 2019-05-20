using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates.Models.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class EnumHelperAttribute : Attribute
    {
        public string DisplayName { get; set; }
    }
}
