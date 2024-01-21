using Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Respawn;
using System.Data.Common;
using Xunit;

namespace Api.IntegrationTests
{
    public class WebApplicationFactoryFixture : IAsyncLifetime
    {
        private readonly WebApplicationFactory<Program> webApplicationFactory;

        private const string connectionString = "Server=localhost;Database=TestingSystem_Tests;User=;Password=;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True";

        private DbConnection dbConnection = default!;

        private Respawner respawner = default!;

        private ApplicationDbContext context;

        public HttpClient HttpClient { get; private set; }

        public WebApplicationFactoryFixture()
        {
            webApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                    services.AddSqlServer<ApplicationDbContext>(connectionString);

                    context =  CreateDbContext(services);
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

        public async Task InitializeAsync()
        {
            HttpClient = webApplicationFactory.CreateClient();

            await InitializeRespawner();
        }

        public async Task ResetDatabase() => await respawner.ResetAsync(dbConnection);

        public new async Task DisposeAsync() => await dbConnection.CloseAsync();

        private async Task InitializeRespawner()
        {
            dbConnection = context.Database.GetDbConnection();
            await dbConnection.OpenAsync();
            respawner = await Respawner.CreateAsync(dbConnection, new RespawnerOptions
            {
                SchemasToInclude = ["dbo"]
            }); ;
        }
    }
}
