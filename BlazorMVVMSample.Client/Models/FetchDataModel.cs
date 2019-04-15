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
        Task RetrieveRealForecastAsync();
        Task RetrieveHourlyForecastAsync();

        IWeatherForecast[] WeatherForecasts { get; }
        IWeatherDotGovForecast RealWeatherForecast { get; }
        IWeatherDotGovForecast HourlyWeatherForecast { get; }
    }

    public class FetchData_Model : IFetchDataModel
    {
        private HttpClient _http;
        private IWeatherForecast[] _weatherForecasts;
        private IWeatherDotGovForecast _realWeatherForecast;
        private IWeatherDotGovForecast _hourlyWeatherForecast;
        private IFullForecastModel _dailyForecast;
        private IBasicForecastModel _basicForecast;
        private IFullForecastModel _hourlyForecast;
        public FetchData_Model(HttpClient http, IEnumerable<IFullForecastModel> fullForecasts, IBasicForecastModel basicForecast)
        {
            _http = http;
            _dailyForecast = fullForecasts.Where(f => f.Supports == supports.daily).First();
            _basicForecast = basicForecast;
            _hourlyForecast = fullForecasts.Where(f => f.Supports == supports.hourly).First();
        }
        
        public IWeatherForecast[] WeatherForecasts { get => _weatherForecasts; private set => _weatherForecasts = value; }
        public IWeatherDotGovForecast RealWeatherForecast { get => _realWeatherForecast; private set => _realWeatherForecast = value; }
        public IWeatherDotGovForecast HourlyWeatherForecast { get => _hourlyWeatherForecast; private set => _hourlyWeatherForecast = value; }

        public async Task RetrieveForecastsAsync()
        {
            if (_weatherForecasts == null)
            {
                _weatherForecasts = await _basicForecast.RetrieveBasicForecast();
            }            
        }

        public async Task RetrieveRealForecastAsync()
        {
            if (_realWeatherForecast == null)
            {
                _realWeatherForecast = await _dailyForecast.RetrieveFullForecast();
            }
        }

        public async Task RetrieveHourlyForecastAsync()
        {
            if (_hourlyWeatherForecast == null)
            {
                _hourlyWeatherForecast = await _hourlyForecast.RetrieveFullForecast();
            }
        }

    }
}
