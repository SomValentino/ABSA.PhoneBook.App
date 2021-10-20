using System;
using ABSA.PhoneBook.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ABSA.PhoneBook.API.Application.Middleware
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigrations(this IApplicationBuilder applicationBuilder)
        {
            var scope = applicationBuilder.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<PhoneBookDbContext>();

            dbContext.Database.Migrate();

            return applicationBuilder;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(option => {
                option.SwaggerEndpoint("/swagger/v1/swagger.json","ABSA.PhoneBook.API v1");
            });

            return applicationBuilder;
        }

        public static IApplicationBuilder UseAPIExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseExceptionHandler(option => {
                option.Run( async context => {
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerPathFeature>();

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new { errorMessage = exception.Error.Message }));
                });
            });

            return applicationBuilder;
        }
    }
}
