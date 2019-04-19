using BlazorMVVM.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.Models
{
    public interface IFetchDataModel
    {
        IWeatherForecast[] WeatherForecasts { get; }

        Task RetrieveForecastsAsync();
    }
    public class FetchDataModel : IFetchDataModel
    {
        private HttpClient _http;
        private IWeatherForecast[] _weatherForecasts;
        public IWeatherForecast[] WeatherForecasts { get => _weatherForecasts; private set => _weatherForecasts = value; }
        public FetchDataModel(HttpClient http)
        {
            _http = http;
        }
        public async Task RetrieveForecastsAsync()
        {
            _weatherForecasts = await _http.GetJsonAsync<WeatherForecast[]>("api/SampleData/WeatherForecasts");
        }
    }
}
