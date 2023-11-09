using Application.Abstraction.Storage;
using Infrastructure.Enums;
using Infrastructure.Services.Storages;
using Infrastructure.Services.Storages.LocalStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceDescriptors)
            where T : Storage, IStorage
        {
            serviceDescriptors.AddScoped<IStorage, T>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceDescriptors, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceDescriptors.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    break;
                case StorageType.AWS:
                    break;
                default:
                    serviceDescriptors.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
