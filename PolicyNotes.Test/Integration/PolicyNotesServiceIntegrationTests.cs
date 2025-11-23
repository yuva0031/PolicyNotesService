using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolicyNotesService.Data;
using PolicyNotesService.Model;
using System.Net;
using System.Net.Http.Json;

namespace PolicyNotes.Test.Integration
{
    public class PolicyNotesServiceIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private static readonly string DbName = "PolicyNotesTestDb";

        public PolicyNotesServiceIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        private HttpClient CreateClient(bool resetDb)
        {
            var factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");

                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<PolicyDbContext>));

                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddDbContext<PolicyDbContext>(options =>
                        options.UseInMemoryDatabase(DbName));
                });
            });

            var client = factory.CreateClient();

            if (resetDb)
            {
                using var scope = factory.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<PolicyDbContext>();
                db.Policies.RemoveRange(db.Policies);
                db.SaveChanges();
            }

            return client;
        }

        [Fact]
        public async Task PostNote_Should_Return_201()
        {
            var client = CreateClient(true);

            var dto = new { PolicyNumber = "PN100", Note = "Test Note" };

            var response = await client.PostAsJsonAsync("/notes", dto);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<Policy>();
            Assert.NotNull(result);
            Assert.True(result!.Id > 0);
        }

        [Fact]
        public async Task GetNotes_Should_Return_200()
        {
            var client = CreateClient(true);

            var response = await client.GetAsync("/notes");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetNoteById_Should_Return_404_When_Not_Found()
        {
            var client = CreateClient(true);

            var response = await client.GetAsync("/notes/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Then_GetById_Should_Return_200()
        {
            var client = CreateClient(true);

            var dto = new { PolicyNumber = "PN101", Note = "Integration Test Note" };

            var post = await client.PostAsJsonAsync("/notes", dto);
            var created = await post.Content.ReadFromJsonAsync<Policy>();

            var get = await client.GetAsync($"/notes/{created!.Id}");

            Assert.Equal(HttpStatusCode.OK, get.StatusCode);
        }
    }
}