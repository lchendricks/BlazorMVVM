using BlazorMVVMSample.Client.Models;
using BlazorMVVMSample.Client.ViewModels;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BlazorMVVMSample.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFetchDataViewModel, FetchDataViewModel>();            
            services.AddTransient<IBasicForecastViewModel, BasicForecastViewModel>();

            //services.AddTransient<IFetchDataModel, FetchData_Model>();
            //services.AddTransient<IFullForecastModel, DailyForecast_Model>();
            //services.AddTransient<IBasicForecastModel, DailyForecast_Model>();
            //services.AddTransient<IFullForecastModel, HourlyForecast_Model>();

            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.StartsWith("BlazorMVVMSample.Client"))
                .First();
            var classes = assembly.ExportedTypes
                .Where(a => a.FullName.Contains("_Model"));
            foreach (Type t in classes)
            {
                foreach (Type i in t.GetInterfaces())
                {
                    services.AddTransient(i, t);
                }
            }

        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
