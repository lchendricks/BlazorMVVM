using BlazorMVVM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.ViewModels
{
    public delegate Task ToggleDelegate(DateTime selectedDay);
    public interface IBasicForecastViewModel
    {
        string DisplayTemperatureScaleShort { get; }
        IWeatherForecast[] WeatherForecasts { get; set; }
        ToggleDelegate ToggleForecastDelegate { get; set; }

        int DisplayTemperature(int temperature);
        Task ToggleForecast(DateTime selectedDate);
        void ToggleTemperatureScale();
    }

    public class BasicForecastViewModel : IBasicForecastViewModel
    {
        private IWeatherForecast[] _weatherForecasts;
        private bool _displayFahrenheit;

        public IWeatherForecast[] WeatherForecasts { get => _weatherForecasts; set => _weatherForecasts = value; }

        public string DisplayTemperatureScaleShort
        {
            get { return _displayFahrenheit ? "C" : "F"; }
        }
        public ToggleDelegate ToggleForecastDelegate { get; set; }
        public int DisplayTemperature(int temperature)
        {
            return _displayFahrenheit ? temperature : 32 + (int)(temperature / 0.5556);
        }

        public void ToggleTemperatureScale()
        {
            _displayFahrenheit = !_displayFahrenheit;
        }
        public async Task ToggleForecast(DateTime selectedDate)
        {
            await ToggleForecastDelegate(selectedDate);
        }
    }
}
