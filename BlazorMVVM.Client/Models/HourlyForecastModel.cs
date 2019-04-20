using BlazorMVVM.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.Models
{
    public class HourlyForecast_Model : IFullForecastModel
    {
        private HttpClient _http;
        public supports Supports { get; private set; }
        public HourlyForecast_Model(HttpClient http)
        {
            _http = http;
            Supports = supports.hourly;
        }
        public async Task<IWeatherDotGovForecast> RetrieveFullForecast()
        {
            var forecast = await _http.GetJsonAsync<WeatherDotGovForecast>("https://api.weather.gov/gridpoints/ALY/59,14/forecast/hourly");
            return forecast;
        }
    }
}
