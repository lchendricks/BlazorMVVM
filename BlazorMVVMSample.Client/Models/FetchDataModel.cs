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
        Task RetrieveForecastsAsync();
        IWeatherForecast[] WeatherForecasts { get; }
    }

    public class FetchDataModel : IFetchDataModel
    {
        private HttpClient _http;
        private IWeatherForecast[] _weatherForecasts;  
        public FetchDataModel(HttpClient http)
        {
            _http = http;           
        }

        public IWeatherForecast[] WeatherForecasts { get => _weatherForecasts; private set => _weatherForecasts = value; }

        public async Task RetrieveForecastsAsync()
        {
            Console.WriteLine("FetchDataModel Retrieving Forecasts");
            _weatherForecasts = await _http.GetJsonAsync<WeatherForecast[]>("api/SampleData/WeatherForecasts");
            Console.WriteLine("FetchDataModel Forecasts Retrieved");
        }

    }
}
