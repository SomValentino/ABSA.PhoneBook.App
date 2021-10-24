using System;
using System.Threading.Tasks;
using ABSA.PhoneBook.Data.Context;
using ABSA.PhoneBook.Data.Repository;
using ABSA.PhoneBook.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ABSA.PhoneBook.API.Tests.Setup
{
    public class PhoneBookApplicationFactory<TStartup>
                    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddImemoryDatabase();
            }).UseEnvironment("Test");
        }
    }


}
