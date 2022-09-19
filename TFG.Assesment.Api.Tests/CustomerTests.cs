using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TFG.Assesment.Api.Tests.Responses;
using TFG.Assessment.Domain.Entities;
using TFG.Assessment.Domain.Requests;
using TFG.Assessment.EFCore;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace TFG.Assesment.Api.Tests
{
    public class CustomerTests: IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public CustomerTests(TestingWebAppFactory<Program> factory)
        {
            var appFactory = new WebApplicationFactory<Program>();
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task AddCustomer_WhenCalledWithValidRole_ReturnsSuccess()
        {
            await PerformLogin("TestUser", "password");
            var customer = new AddCustomer { FirstName = "UnitTestFirstName", LastName = "UnitTestLastName" };
            var response = await _client.PostAsJsonAsync<AddCustomer>("/api/customers", customer);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task AddCustomer_WhenCalledWithNoRole_ReturnsUnSuccessfull()
        {
            var customer = new AddCustomer { FirstName = "UnitTestFirstName", LastName = "UnitTestLastName" };
            var response = await _client.PostAsJsonAsync<AddCustomer>("/api/customers", customer);
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task UpdateCustomer_WhenCalledWithValidRole_ReturnsSuccess()
        {
            await PerformLogin("TestUser", "password");
            var customer = new UpdateCustomer { FirstName = "UnitTestFirstName", LastName = "UnitTestLastName" };
            var response = await _client.PutAsJsonAsync("/api/customers/1", customer);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task UpdateCustomer_WhenCalledWithInvalidRole_ReturnsSuccess()
        {
            var customer = new UpdateCustomer { FirstName = "UnitTestFirstName", LastName = "UnitTestLastName" };
            var response = await _client.PutAsJsonAsync("/api/customers/1", customer);
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task DeleteCustomer_WhenCalledWithValidRole_ReturnsSuccess()
        {
            await PerformLogin("Admin", "password");
            var customer = new AddCustomer { FirstName = "UnitTestFirstName", LastName = "UnitTestLastName" };
            var response = await _client.DeleteAsync("/api/customers/1");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task DeleteCustomer_WhenCalledWithInvalidRole_ReturnsUnSuccessfull()
        {
            await PerformLogin("TestUser", "password");
            var customer = new AddCustomer { FirstName = "UnitTestFirstName", LastName = "UnitTestLastName" };
            var response = await _client.DeleteAsync("/api/customers/1");
            Assert.False(response.IsSuccessStatusCode);
        }


        private async Task PerformLogin(string userName, string password)
        {
            var user = new AuthenticationRequest
            {
                Username = userName,
                Password = password,             
            };

            var res = await _client.PostAsJsonAsync("api/Auth/authenticate", user);
            var responseString = await res.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<TestTokenResponse>(responseString);
            addAuthToken(parsedResponse.Result.JwtToken);
        }


        [Fact]
        public async Task GetAll_WhenCalled_ReturnsAllCustomers()
        {
            var response = await _client.GetAsync("/api/Customers");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<List<Customer>>(responseString);
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(parsedResponse);
        }

        [Fact]
        public async Task GetCustomerById_WhenCalled_ReturnsCustomer()
        {
            var response = await _client.GetAsync("/api/Customers/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<Customer>(responseString);
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(parsedResponse);
            Assert.Equal("Admin", parsedResponse?.FirstName);
        }

        [Fact]
        public async Task FidCustomerBySearch_WhenCalled_ReturnsAllMatchedCustomers()
        {
            var response = await _client.GetAsync("api/Customers/search?searchTerm=admin");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<List<Customer>>(responseString);
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(parsedResponse);
            Assert.Contains(parsedResponse, x => x.FirstName.ToLower() == "admin");
        }

        private void addAuthToken(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
