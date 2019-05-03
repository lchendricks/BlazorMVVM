using BlazorMVVM.Client.Models;
using BlazorMVVM.Client.ViewModels;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BlazorMVVM.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFetchDataViewModel, FetchDataViewModel>();
            services.AddTransient<IBasicForecastViewModel, BasicForecastViewModel>();
            services.AddTransient<IShoppingCartViewModel, ShoppingCartViewModel>();     

            var assembly = AppDomain.CurrentDomain.GetAssemblies()
               .Where(a => a
               .FullName.StartsWith("BlazorMVVM.Client"))
               .First();
            var classes = assembly.ExportedTypes
               .Where(a => a.FullName.Contains("_Model"));
            classes = classes.Union(assembly.ExportedTypes
               .Where(a => a.FullName.Contains("_ViewModel")));
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
