using BlazorMVVM.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.Models
{
    public interface IFetchDataModel
    {
        IWeatherForecast[] WeatherForecasts { get; }
        WeatherDotGovForecast RealWeatherForecast { get; }
        WeatherDotGovForecast HourlyWeatherForecast { get; }

        Task RetrieveForecastsAsync();
        Task RetrieveHourlyForecastAsync();
        Task RetrieveRealForecastAsync();
    }
    public class FetchDataModel : IFetchDataModel
    {
        private HttpClient _http;
        private IWeatherForecast[] _weatherForecasts;
        private WeatherDotGovForecast _realWeatherForecast;
        private WeatherDotGovForecast _hourlyWeatherForecast;
        public IWeatherForecast[] WeatherForecasts { get => _weatherForecasts; private set => _weatherForecasts = value; }
        public WeatherDotGovForecast RealWeatherForecast { get => _realWeatherForecast; private set => _realWeatherForecast = value; }
        public WeatherDotGovForecast HourlyWeatherForecast { get => _hourlyWeatherForecast; private set => _hourlyWeatherForecast = value; }
        public FetchDataModel(HttpClient http)
        {
            _http = http;
        }
        public async Task RetrieveForecastsAsync()
        {
            _weatherForecasts = await _http.GetJsonAsync<WeatherForecast[]>("api/SampleData/WeatherForecasts");
        }
        public async Task RetrieveRealForecastAsync()
        {
            _realWeatherForecast = await _http.GetJsonAsync<WeatherDotGovForecast>
              ("https://api.weather.gov/gridpoints/ALY/59,14/forecast");
        }
        public async Task RetrieveHourlyForecastAsync()
        {
            _hourlyWeatherForecast = await _http.GetJsonAsync<WeatherDotGovForecast>("https://api.weather.gov/gridpoints/ALY/59,14/forecast/hourly");
        }
    }
}
