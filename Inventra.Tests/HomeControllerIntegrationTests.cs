using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Inventra.Tests
{
    public class HomeControllerIntegrationTests : IClassFixture<InventraWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public HomeControllerIntegrationTests(InventraWebApplicationFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task HomePage_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/");

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task LoginPage_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/Account/Login");

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task InventoryDetails_Unauthenticated_RedirectsToLogin()
        {
            var response = await _client.GetAsync("/Inventory/Create");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Redirect);
            response.Headers.Location!.ToString().Should().Contain("/Account/Login");
        }
    }
}
