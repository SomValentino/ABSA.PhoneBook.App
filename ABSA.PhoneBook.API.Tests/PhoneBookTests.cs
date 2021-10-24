using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ABSA.PhoneBook.API.Application.Dto.Request;
using ABSA.PhoneBook.API.Application.Dto.Response;
using ABSA.PhoneBook.API.Tests.Setup;
using ABSA.PhoneBook.Data.Context;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Xunit;

namespace ABSA.PhoneBook.API.Tests
{
    public class PhoneBookTests
    {
        private readonly PhoneBookApplicationFactory<Startup> _factory;

        public PhoneBookTests()
        {
            _factory = new PhoneBookApplicationFactory<Startup>();  
        }

        [Fact]
        public async Task PhoneBookController_Get_ReturnsPhoneBookList()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/phonebook");

            var responseString = await response.Content.ReadAsStringAsync();

            var phonebooks = JsonConvert.DeserializeObject<PhoneBookDto>(responseString);

            Assert.NotNull(phonebooks);
            Assert.NotEmpty(phonebooks.PhoneBooks);
        }

        [Fact]
        public async Task PhoneBookController_GetById_ReturnsPhoneBook()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"api/phonebook/{1}");

            var responseString = await response.Content.ReadAsStringAsync();

            var phoneBook = JsonConvert.DeserializeObject<Domain.Entities.PhoneBook>(responseString);

            Assert.NotNull(phoneBook);
            Assert.NotNull(phoneBook.Name);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PhoneBookController_GetByWrongId_ReturnsNotFoundErrorMessage()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"api/phonebook/{int.MaxValue}");

            var responseString = await response.Content.ReadAsStringAsync();

            var error = JsonConvert.DeserializeObject<ErrorModel>(responseString);

            Assert.NotNull(error);
            Assert.NotNull(error.ErrorMessage);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PhoneBookController_Create_ReturnsCreatedMessage()
        {
            var client = _factory.CreateClient();

            var request = JsonConvert.SerializeObject(new PhoneBookCreateDto
            {
                Name = "TestBook3"
            });

            var srequested = new StringContent(request,Encoding.UTF8,"application/json");

            var response = await client.PostAsync("api/phonebook", srequested);

            var responseString = await response.Content.ReadAsStringAsync();

            var phoneBook = JsonConvert.DeserializeObject<Domain.Entities.PhoneBook>(responseString);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(phoneBook);
        }

        [Fact]
        public async Task PhoneBookController_CreateExistingPhoneBook_ReturnsCreatedMessage()
        {
            var client = _factory.CreateClient();

            var request = JsonConvert.SerializeObject(new PhoneBookCreateDto
            {
                Name = "TestBook1"
            });

            var srequested = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/phonebook", srequested);

            var responseString = await response.Content.ReadAsStringAsync();

            var error = JsonConvert.DeserializeObject<ErrorModel>(responseString);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(error.ErrorMessage);
        }


        [Fact]
        public async Task PhoneBookController_UpdatePhoneBook_ReturnsNoContent()
        {
            var client = _factory.CreateClient();

            var request = JsonConvert.SerializeObject(new PhoneBookCreateDto
            {
                Name = "TestBook3"
            });

            var srequested = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/phonebook/{1}", srequested);

            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task PhoneBookController_UpdatePhoneBookWithWrongId_ReturnsNotFound()
        {
            var client = _factory.CreateClient();

            var request = JsonConvert.SerializeObject(new PhoneBookCreateDto
            {
                Name = "TestBook3"
            });

            var srequested = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/phonebook/{int.MaxValue}", srequested);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PhoneBookController_DeletePhoneBook_ReturnsNotContent()
        {
            var client = _factory.CreateClient();

           
            var response = await client.DeleteAsync($"api/phonebook/{1}");

            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task PhoneBookController_DeletePhoneBookWithWrongId_ReturnsNotFound()
        {
            var client = _factory.CreateClient();


            var response = await client.DeleteAsync($"api/phonebook/{int.MaxValue}");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }




    }
}
