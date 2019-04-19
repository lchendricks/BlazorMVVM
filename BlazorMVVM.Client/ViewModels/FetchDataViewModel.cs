using BlazorMVVM.Client.Models;
using BlazorMVVM.Shared;
using System;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.ViewModels
{
    public interface IFetchDataViewModel
    {
        WeatherForecast[] WeatherForecasts { get; set; }
        Task RetrieveForecastsAsync();
    }
    public class FetchDataViewModel : IFetchDataViewModel
    {
        private WeatherForecast[] _weatherForecasts;
        private IFetchDataModel _fetchDataModel;
        public WeatherForecast[] WeatherForecasts { get => _weatherForecasts; set => _weatherForecasts = value; }

        public FetchDataViewModel(IFetchDataModel fetchDataModel)
        {
            Console.WriteLine("FetchDataViewModel Constructor Executing");
            _fetchDataModel = fetchDataModel;
        }

        public async Task RetrieveForecastsAsync()
        {
            _weatherForecasts = await _fetchDataModel.RetrieveForecastsAsync();
            Console.WriteLine("FetchDataViewModel Forecasts Retrieved");
        }
    }
}
