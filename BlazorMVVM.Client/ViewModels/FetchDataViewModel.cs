using BlazorMVVM.Client.Models;
using BlazorMVVM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.ViewModels
{
    public interface IFetchDataViewModel
    {
        string DisplayOtherTemperatureScaleLong { get; }
        IBasicForecastViewModel BasicForecastViewModel { get; }
        string DisplayPremiumToggleMessage { get; }
        Task RetrieveForecastsAsync();
        Task TogglePremiumMembership();
        void ToggleTemperatureScale();
    }
    public class FetchDataViewModel : IFetchDataViewModel
    {
        private IFetchDataModel _fetchDataModel;
        private bool _displayFahrenheit;
        private IBasicForecastViewModel _basicForecastViewModel;
        private bool _isPremiumMember;
        private bool _isDailyForecast;

        public string DisplayOtherTemperatureScaleLong
        {
            get
            {
                return _displayFahrenheit ? "Celsius" : "Fahrenheit";
            }
        }
        public IBasicForecastViewModel BasicForecastViewModel
        {
            get => _basicForecastViewModel;
            private set => _basicForecastViewModel = value;
        }
        public string DisplayPremiumToggleMessage => _isPremiumMember ? "Change to Basic" : "Change to Premium";

        public FetchDataViewModel(IFetchDataModel fetchDataModel, IBasicForecastViewModel basicForecastViewModel)
        {
            Console.WriteLine("FetchDataViewModel Constructor Executing");
            _fetchDataModel = fetchDataModel;
            _basicForecastViewModel = basicForecastViewModel;
            _displayFahrenheit = true;
            _isPremiumMember = false;
            basicForecastViewModel.ToggleForecastDelegate = ToggleForecast;
            _isDailyForecast = true;
        }

        public async Task RetrieveForecastsAsync()
        {
            List<IWeatherForecast> newForecasts = new List<IWeatherForecast>();
            if (_isPremiumMember)
            {
                await PopulateEnhancedForecastData(newForecasts);
            }
            else
            {
                await PopulateStandardForecastData(newForecasts);
            }
            _basicForecastViewModel.WeatherForecasts = newForecasts.ToArray();
            Console.WriteLine("FetchDataViewModel Forecasts Retrieved");
        }

        private async Task PopulateStandardForecastData(List<IWeatherForecast> newForecasts)
        {
            await _fetchDataModel.RetrieveForecastsAsync();
            foreach (IWeatherForecast forecast in _fetchDataModel.WeatherForecasts)
            {
                IWeatherForecast newForecast = new WeatherForecast();
                newForecast.Date = forecast.Date;
                newForecast.Summary = forecast.Summary;
                newForecast.TemperatureC = forecast.TemperatureC;
                newForecasts.Add(forecast);
            }
        }
        private async Task PopulateEnhancedForecastData(List<IWeatherForecast> newForecasts)
        {
            List<Period> forecasts = new List<Period>();
            if (_isDailyForecast)
            {
                await _fetchDataModel.RetrieveRealForecastAsync();
                forecasts.AddRange(_fetchDataModel.RealWeatherForecast.properties.periods);
            }
            else
            {
                await _fetchDataModel.RetrieveHourlyForecastAsync();
                forecasts.AddRange(_fetchDataModel.HourlyWeatherForecast.properties.periods);
            }
            foreach (Period forecast in forecasts)
            {
                IWeatherForecast newForecast = new WeatherForecast();
                newForecast.Date = forecast.startTime;
                if (_isDailyForecast)
                {
                    newForecast.Summary = forecast.name + " - ";
                }
                else
                {
                    newForecast.Summary = "";
                }
                newForecast.Summary = newForecast.Summary + forecast.shortForecast;
                newForecast.TemperatureC = (int)((forecast.temperature - 32) * 5 / 9);
                newForecasts.Add(newForecast);
            }
        }

        public void ToggleTemperatureScale()
        {
            _displayFahrenheit = !_displayFahrenheit;
            _basicForecastViewModel.ToggleTemperatureScale();
        }
        public async Task TogglePremiumMembership()
        {
            _isPremiumMember = !_isPremiumMember;
            await RetrieveForecastsAsync();
        }
        private async Task ToggleForecast(DateTime selectedDate)
        {
            if (_isPremiumMember)
            {
                _isDailyForecast = !_isDailyForecast;
                List<IWeatherForecast> newForecasts = new List<IWeatherForecast>();
                await PopulateEnhancedForecastData(newForecasts);
                if (_isDailyForecast)
                {
                    _basicForecastViewModel.WeatherForecasts = newForecasts.ToArray();
                }
                else
                {
                    _basicForecastViewModel.WeatherForecasts = newForecasts
                    .Where(nf => nf.Date.ToShortDateString() == selectedDate.ToShortDateString())
                    .ToArray();
                }
            }
        }
    }
}
