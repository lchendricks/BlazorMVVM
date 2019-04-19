using BlazorMVVM.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.Models
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
