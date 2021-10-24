using System;
using System.Linq;
using ABSA.PhoneBook.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ABSA.PhoneBook.API.Tests.Setup
{
    public static class Extensions
    {
        public static void AddImemoryDatabase(this IServiceCollection service)
        {
            if (service.Any(x => x.ServiceType == typeof(PhoneBookDbContext)))
            {
                var serviceDescriptors = service.Where(x => x.ServiceType == typeof(DbContextOptions<PhoneBookDbContext>)).ToList();

                foreach (var serviceDescriptor in serviceDescriptors)
                {
                    service.Remove(serviceDescriptor);
                }
            }

            service.AddDbContext<PhoneBookDbContext>((_,context) => context.UseInMemoryDatabase("PhoneBookDb"));

            var serverScope = service.BuildServiceProvider().CreateScope();

            var dbContext = serverScope.ServiceProvider.GetRequiredService<PhoneBookDbContext>();

            dbContext.Database.EnsureCreated();

            SeedData(dbContext);


        }

        public static void SeedData(PhoneBookDbContext phoneBookDbContext)
        {
            phoneBookDbContext.Database.EnsureDeleted();

            phoneBookDbContext.PhoneBooks.AddRange(
                new Domain.Entities.PhoneBook
                {
                    Name = "TestBook1",
                    CreatedAt = DateTime.Now,
                    Entries = new List<Domain.Entities.PhoneBookEntry>
                    {
                        new Domain.Entities.PhoneBookEntry
                        {
                            Name = "Test1",
                            PhoneNumber = "0890000001"
                        }
                    }
                },
                new Domain.Entities.PhoneBook
                {
                    Name = "TestBook2",
                    CreatedAt = DateTime.Now,
                    Entries = new List<Domain.Entities.PhoneBookEntry>
                    {
                        new Domain.Entities.PhoneBookEntry
                        {
                            Name = "Test2",
                            PhoneNumber = "0890000002"
                        }
                    }
                }
            );

            phoneBookDbContext.SaveChanges();
        }
    }
}
