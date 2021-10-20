using System.IO;
using System;
using System.Reflection;
using ABSA.PhoneBook.Data.Context;
using ABSA.PhoneBook.Data.Repository;
using ABSA.PhoneBook.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ABSA.PhoneBook.API.Application.Services;

namespace ABSA.PhoneBook.API.Application.IoC {
    public static class DependencyInjection {
        public static IServiceCollection AddPhoneBookDbContext (this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<PhoneBookDbContext> (options => options.UseSqlServer (configuration.GetConnectionString ("PhoneBookConnection"),
                sqlServerOptionsAction : sqlOptions => {
                    sqlOptions.MigrationsAssembly (typeof (Startup).GetTypeInfo ().Assembly.GetName ().Name);
                }));
            return services;
        }

        public static IServiceCollection AddDataLayerInfrastructure (this IServiceCollection services) {
            services.AddScoped<IPhoneBookRepository, PhoneBookRepository> ();
            services.AddScoped<IPhoneBookEntryRepository, PhoneBookEntryRepository> ();

            return services;
        }

        public static IServiceCollection AddServiceInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPhoneBookService,PhoneBookService>();
            services.AddScoped<IPhoneBookEntryService, PhoneBookEntryService>();
            
            return services;
        }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(option => {
                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{
                    Title = "ABSA.PhoneBook.API",
                    Version = "v1"
                });
            });

            return services;
        }
    }
}