using Enterprise.Template.Application.IntegrationTests.Core.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Template.Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static ITestDatabase? _database;
        private static CustomWebApplicationFactory _factory = null!;
        private static IServiceScopeFactory _scopeFactory = null!;

        public static IServiceScopeFactory ScopeFactory => _scopeFactory;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            _database = new SqlServerTestDatabase();
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            await _database.InitialiseAsync();
        }

        public static async Task ResetState()
        {
            if (_database != null)
                await _database.ResetAsync();
        }

        [OneTimeTearDown]
        public async Task RunAfterAnyTests()
        {
            await _factory.DisposeAsync();
        }
    }
}