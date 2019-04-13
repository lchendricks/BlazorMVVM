using BlazorMVVMSample.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMVVMSample.Client.ViewModels
{
    public delegate Task ToggleDelegate(DateTime selectedDay);
    public interface IBasicForecastViewModel
    {
        IWeatherForecast[] WeatherForecasts { get; set; }
        string DisplayTemperatureScaleShort { get; }
        ToggleDelegate ToggleForecastDelegate { get; set; }

        int DisplayTemperature(int temperature);
        void ToggleTemperatureScale();
        Task ToggleForecast(DateTime selectedDate);
        
    }

    public class BasicForecastViewModel : IBasicForecastViewModel
    {
        public ToggleDelegate ToggleForecastDelegate { get; set; }

        private IWeatherForecast[] _weatherForecasts;
        private bool _displayFahrenheit;        

        public IWeatherForecast[] WeatherForecasts { get => _weatherForecasts; set => _weatherForecasts = value; }

        public string DisplayTemperatureScaleShort
        {
            get
            {
                return _displayFahrenheit ? "F" : "C";
            }
        }

        public BasicForecastViewModel()
        {
            _displayFahrenheit = true;
        }

        public int DisplayTemperature(int temperature)
        {
            return _displayFahrenheit ? 32 + (int)(temperature / 0.5556) : temperature;
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
