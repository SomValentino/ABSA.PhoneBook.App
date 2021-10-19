using System;
using System.Reflection;
using ABSA.PhoneBook.Data.Context;
using ABSA.PhoneBook.Data.Repository;
using ABSA.PhoneBook.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ABSA.PhoneBook.API.Application.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPhoneBookDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PhoneBookDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("PhoneBookConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));
            return services;
        }

        public static IServiceCollection AddDataLayerInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPhoneBookRepository, PhoneBookRepository>();
            services.AddScoped<IPhoneBookEntryRepository, PhoneBookEntryRepository>();

            return services;
        }


    }
}
