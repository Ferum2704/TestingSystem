using Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Api.IntegrationTests
{
    public class WebApplicationFactoryFixture
    {
        private readonly WebApplicationFactory<Program> webApplicationFactory;

        private const string connectionString = "Server=localhost;Database=TestingSystem_Tests;User=;Password=;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True";

        public HttpClient HttpClient { get; private set; }

        public WebApplicationFactoryFixture()
        {
            webApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                    services.AddSqlServer<ApplicationDbContext>(connectionString);

                    var dbContext = CreateDbContext(services);
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();
                });
            });

            HttpClient = webApplicationFactory.CreateClient();
        }

        public IServiceScope CreateScope() => webApplicationFactory.Services.CreateScope();

        private static ApplicationDbContext CreateDbContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }
    }
}
