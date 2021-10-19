using System;
using ABSA.PhoneBook.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ABSA.PhoneBook.API.Application.Middleware
{
    public static class Extensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder applicationBuilder)
        {
            var scope = applicationBuilder.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<PhoneBookDbContext>();

            dbContext.Database.Migrate();

            return applicationBuilder;
        }
    }
}
