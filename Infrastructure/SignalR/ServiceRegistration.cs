using Application.Abstraction.Hubs;
using Microsoft.Extensions.DependencyInjection;
using SignalR.HubService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddScoped<IProductHubService, ProductHubService>();
            services.AddScoped<IOrderHubService, OrderHubService>();
            services.AddSignalR();
        }
    }
}
