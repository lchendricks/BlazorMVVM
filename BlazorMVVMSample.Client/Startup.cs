using BlazorMVVMSample.Client.Models;
using BlazorMVVMSample.Client.ViewModels;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorMVVMSample.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFetchDataViewModel, FetchDataViewModel>();
            services.AddTransient<IFetchDataModel, FetchDataModel>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
