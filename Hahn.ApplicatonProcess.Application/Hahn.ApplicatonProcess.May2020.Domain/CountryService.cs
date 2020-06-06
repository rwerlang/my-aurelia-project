using Hahn.ApplicatonProcess.May2020.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Domain
{
    /// <summary>
    /// Class used to retrieve a valid list of countries
    /// </summary>
    public class CountryService
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Creates a new instance of the object
        /// </summary>
        /// <param name="client">http client used to pull countries</param>
        public CountryService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Returns an object with the country information if found
        /// </summary>
        /// <param name="name">Name of the country</param>
        /// <returns>CountryModel</returns>
        public Task<List<CountryModel>> SearchCountryByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return SearchCountryByNameAsyncRun(name);
        }

        private async Task<List<CountryModel>> SearchCountryByNameAsyncRun(string name)
        {
            string url = string.Format("https://restcountries.eu/rest/v2/name/{0}?fullText=true&fields=name;alpha3Code", name);
            using (var response = await _client.GetAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(result))
                {
                    return null;
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<CountryModel>>(result);
            }
        }

    }
}
