using CaliberFS.Template.Application.Tests.Core.Tests;
using CaliberFS.Template.Data.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CaliberFS.Template.Application.Tests
{
    [SetUpFixture]
    public class Testing
    {
        private static ITestDatabase? _database;
        private static CustomWebApplicationFactory _factory = null!;
        private static IServiceScopeFactory _scopeFactory = null!;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            _database = new SqlServerTestDatabase();
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            await _database.InitialiseAsync(_scopeFactory);
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>()!;
            return await mediator.Send(request);
        }

        public static async Task AddManyAsync(List<Domain.Entities.Board> entities)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            foreach (var item in entities)
            {
                await context.BoardRepository.AddAsync(item);
            }
        }

        public static async Task ResetState()
        {
            if (_database != null)
            {
                _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
                await _database.ResetAsync(_scopeFactory);
            }
        }

        [OneTimeTearDown]
        public async Task RunAfterAnyTests()
        {
            await _factory.DisposeAsync();
        }
    }
}