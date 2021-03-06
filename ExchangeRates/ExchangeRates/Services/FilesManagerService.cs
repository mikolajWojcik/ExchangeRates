﻿using ExchangeRates.Models;
using ExchangeRates.Models.Enums;
using ExchangeRates.Services.Interfaces;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeRates.Services
{
    public class FilesManagerService : IFilesManagerService
    {
        public async Task<SortedDictionary<DateTime, Dictionary<CurrencyType, double>>> GetDataStoredOffllineAsync(CurrencyType baseCurrency)
        {
            var fileName = CreateFileNameForBase(baseCurrency.ToString());

            try
            {
                if (File.Exists(fileName))
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var streamReader = new StreamReader(fileStream))
                        {
                            var json = await streamReader.ReadToEndAsync();
                            return JsonConvert.DeserializeObject<SortedDictionary<DateTime, Dictionary<CurrencyType, double>>>(json);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                File.Delete(fileName);
            }

            return new SortedDictionary<DateTime, Dictionary<CurrencyType, double>>();          
        }

        public async Task SaveRatesAsync(CurrencyType baseCurrency, SortedDictionary<DateTime, Dictionary<CurrencyType, double>> rates)
        {
            var jsonToSave = JsonConvert.SerializeObject(rates);
            var fileName = CreateFileNameForBase(baseCurrency.ToString());

            File.Delete(fileName);

            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    await streamWriter.WriteAsync(jsonToSave);
                }
            }         
        }

        private string CreateFileNameForBase(string name)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{name}.json");
        }
    }
}
