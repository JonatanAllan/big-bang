using CaliberFS.Template.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CaliberFS.Template.Application.Tests.Core.Tests
{
    public class SqlServerTestDatabase : ITestDatabase
    {
        private IDbSession _dbSession;

        public async Task InitialiseAsync(IServiceScopeFactory serviceScopeFactory)
        {
            _dbSession = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IDbSession>();
            var command = _dbSession.Connection.CreateCommand();

            command.CommandText = "CREATE TABLE IF NOT EXISTS Boards (Id int, Name varchar(200), Description varchar(2000))";
            command.ExecuteNonQuery();

            _dbSession.Connection.Close();
        }

        public async Task ResetAsync(IServiceScopeFactory serviceScopeFactory)
        {
            _dbSession = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IDbSession>();
            var command = _dbSession.Connection.CreateCommand();

            command.CommandText = "delete from Boards";
            command.ExecuteNonQuery();

            _dbSession.Connection.Close();
        }

        private void SeedData()
        {
            // Seed data
        }
    }
}

