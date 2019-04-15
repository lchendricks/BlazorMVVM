using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMVVMSample.Shared
{
    public interface IWeatherDotGovForecast
    {
        Geometry geometry { get; set; }
        Properties properties { get; set; }
        string type { get; set; }
    }

    public class WeatherDotGovForecast : IWeatherDotGovForecast
    {
        //public List<object> __invalid_name__ { get; set; }
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }
    public class Geometry2
    {
        public string type { get; set; }
        public List<object> coordinates { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<Geometry2> geometries { get; set; }
    }

    public class Elevation
    {
        public double value { get; set; }
        public string unitCode { get; set; }
    }

    public class Period
    {
        public int number { get; set; }
        public string name { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public bool isDaytime { get; set; }
        public int temperature { get; set; }
        public string temperatureUnit { get; set; }
        public object temperatureTrend { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
        public string icon { get; set; }
        public string shortForecast { get; set; }
        public string detailedForecast { get; set; }
    }

    public class Properties
    {
        public DateTime updated { get; set; }
        public string units { get; set; }
        public string forecastGenerator { get; set; }
        public DateTime generatedAt { get; set; }
        public DateTime updateTime { get; set; }
        public string validTimes { get; set; }
        public Elevation elevation { get; set; }
        public List<Period> periods { get; set; }
    }

    
}
