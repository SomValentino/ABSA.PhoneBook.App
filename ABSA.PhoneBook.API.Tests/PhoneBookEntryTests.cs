using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ABSA.PhoneBook.API.Application.Dto.Request;
using ABSA.PhoneBook.API.Application.Dto.Response;
using ABSA.PhoneBook.API.Tests.Setup;
using ABSA.PhoneBook.Domain.Entities;
using Newtonsoft.Json;
using Xunit;

namespace ABSA.PhoneBook.API.Tests
{
    public class PhoneBookEntryTests
    {
        private readonly PhoneBookApplicationFactory<Startup> _factory;

        public PhoneBookEntryTests()
        {
            _factory = new PhoneBookApplicationFactory<Startup>();
        }

        [Fact]
        public async Task PhoneBookController_GetEntry_ReturnsListOfPhoneBookEntry()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"api/phonebook/{1}/entries");

            var responseString = await response.Content.ReadAsStringAsync();

            var entries = JsonConvert.DeserializeObject<PhoneBookEntryDto>(responseString);

            Assert.NotNull(entries);
            Assert.NotEmpty(entries.PhoneBookEntries);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PhoneBookController_GetEntryWithWrongPhoneBookId_ReturnsNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"api/phonebook/{int.MaxValue}/entries");

            
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PhoneBookController_GetEntryWithId_ReturnsPhoneBookEntry()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"api/phonebook/entry/{1}");

            var responseString = await response.Content.ReadAsStringAsync();

            var entry = JsonConvert.DeserializeObject<PhoneBookEntry>(responseString);

            Assert.NotNull(entry.Name);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PhoneBookController_GetEntryWithWrongId_ReturnsNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"api/phonebook/entry/{int.MaxValue}");

            
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PhoneBookController_CreateEntry_ReturnsCreatedEnrtry()
        {
            var client = _factory.CreateClient();

            var request = JsonConvert.SerializeObject(new PhoneBookEntryCreateDto
            {
                Name = "Test3",
                PhoneNumber = "0890000004"
            });

            var srequest = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"api/phonebook/{1}/entry",srequest);

            var responseString = await response.Content.ReadAsStringAsync();

            var entry = JsonConvert.DeserializeObject<PhoneBookEntry>(responseString);

            Assert.NotNull(entry);
            Assert.NotEmpty(entry.Name);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PhoneBookController_UpdateEntry_ReturnsNoContent()
        {
            var client = _factory.CreateClient();

            var request = JsonConvert.SerializeObject(new PhoneBookEntryCreateDto
            {
                Name = "Test3",
                PhoneNumber = "0890000004"
            });

            var srequest = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/phonebook/entry/{1}", srequest);

            
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Fact]
        public async Task PhoneBookController_UpdateEntryWithWrongId_ReturnsNotFound()
        {
            var client = _factory.CreateClient();

            var request = JsonConvert.SerializeObject(new PhoneBookEntryCreateDto
            {
                Name = "Test3",
                PhoneNumber = "0890000004"
            });

            var srequest = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/phonebook/entry/{int.MaxValue}", srequest);


            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PhoneBookController_DeleteEntry_ReturnsNoContent()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"api/phonebook/entry/{1}");


            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Fact]
        public async Task PhoneBookController_DeleteEntryWithWrongid_ReturnsNoContent()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"api/phonebook/entry/{int.MaxValue}");


            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
