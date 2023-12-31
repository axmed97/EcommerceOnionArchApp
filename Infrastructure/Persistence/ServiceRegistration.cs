﻿using Application.Abstraction.Services;
using Application.Abstraction.Services.Authentications;
using Application.Repositories.BasketItemRepositories;
using Application.Repositories.BasketRepositories;
using Application.Repositories.CustomerRepositories;
using Application.Repositories.FileRepositories;
using Application.Repositories.InvoiceFileRepositories;
using Application.Repositories.OrderRepositories;
using Application.Repositories.ProductImageFileRepositories;
using Application.Repositories.ProductRepositories;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories.BasketItemRepositories;
using Persistence.Repositories.BasketRepositories;
using Persistence.Repositories.CustomerRepositories;
using Persistence.Repositories.FileRepositories;
using Persistence.Repositories.InvoiceFileRepositories;
using Persistence.Repositories.OrderRepositories;
using Persistence.Repositories.ProductImageFileRepositories;
using Persistence.Repositories.ProductRepositories;
using Persistence.Services;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IFileReadRepositories, FileReadRepositories>();
            services.AddScoped<IFileWriteRepositories, FileWriteRepositories>();

            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository, ProductImageReadFileRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageWriteFileRepository>();

            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();

            services.AddScoped<IBasketService, BasketService>();

            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
