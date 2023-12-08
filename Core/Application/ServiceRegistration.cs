using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;

namespace Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
            services.AddHttpClient();
        }
    }
}
