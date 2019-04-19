using BlazorMVVM.Client.Models;
using BlazorMVVM.Client.ViewModels;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorMVVM.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFetchDataViewModel, FetchDataViewModel>();
            services.AddTransient<IFetchDataModel, FetchDataModel>();
            services.AddTransient<IBasicForecastViewModel, BasicForecastViewModel>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
