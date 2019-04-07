using BlazorMVVMSample.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorMVVMSample.Client.Models
{
    public interface IFetchDataModel
    {
        Task<WeatherForecast[]> RetrieveForecastsAsync();
    }

    public class FetchDataModel : IFetchDataModel
    {
        private HttpClient _http;
        public FetchDataModel(HttpClient http)
        {
            _http = http;
        }

        public async Task<WeatherForecast[]> RetrieveForecastsAsync()
        {
            return await _http.GetJsonAsync<WeatherForecast[]>("api/SampleData/WeatherForecasts");
        }
    }
}
