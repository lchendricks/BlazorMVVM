using BlazorMVVMSample.Client.Models;
using BlazorMVVMSample.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMVVMSample.Client.ViewModels
{
    public interface IFetchDataViewModel
    {
        IWeatherForecast[] WeatherForecasts { get; set; }
        string DisplayTemperatureScaleShort { get; }
        string DisplayOtherTemperatureScaleLong { get; }

        int DisplayTemperature(int temperature);
        Task RetrieveForecastsAsync();
        void ToggleTemperatureScale();
    }

    public class FetchDataViewModel : IFetchDataViewModel
    {
        private IWeatherForecast[] _weatherForecasts;
        private IFetchDataModel _fetchDataModel;
        private bool _displayFahrenheit;

        public IWeatherForecast[] WeatherForecasts { get => _weatherForecasts; set => _weatherForecasts = value; }

        public FetchDataViewModel(IFetchDataModel fetchDataModel)
        {
            Console.WriteLine("FetchDataViewModel Constructor Executing");
            _fetchDataModel = fetchDataModel;
            _displayFahrenheit = true;
        }

        public async Task RetrieveForecastsAsync()
        {
            Console.WriteLine("FetchDataViewModel Retrieving Forecasts");
            await _fetchDataModel.RetrieveForecastsAsync();
            List<IWeatherForecast> newForecasts = new List<IWeatherForecast>();
            foreach (IWeatherForecast forecast in _fetchDataModel.WeatherForecasts)
            {
                IWeatherForecast newForecast = new WeatherForecast();
                newForecast.Date = forecast.Date;
                newForecast.Summary = forecast.Summary;
                newForecast.TemperatureC = forecast.TemperatureC;
                newForecasts.Add(forecast);
            }
            _weatherForecasts = newForecasts.ToArray();

            Console.WriteLine("FetchDataViewModel Forecasts Retrieved");
        }


        public string DisplayTemperatureScaleShort
        {
            get
            {
                return _displayFahrenheit ? "F" : "C";
            }
        }

        public string DisplayOtherTemperatureScaleLong
        {
            get
            {
                return !_displayFahrenheit ? "Fahrenheit" : "Celsius";
            }
        }

        public int DisplayTemperature(int temperature)
        {
            return _displayFahrenheit ? 32 + (int)(temperature / 0.5556) : temperature;
        }

        public void ToggleTemperatureScale()
        {
            _displayFahrenheit = !_displayFahrenheit;
        }
    }
}
