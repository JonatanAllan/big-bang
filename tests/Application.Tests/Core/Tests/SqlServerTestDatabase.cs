using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Infrastructure.Context;
using Respawn;

namespace Application.Tests.Core.Fixture
{
    public class SqlServerTestDatabase : ITestDatabase
    {
        private AppDbContext _context = null!;
        private const string DatabaseName = "sample";

        public async Task InitialiseAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(DatabaseName)
            .Options;

            _context = new AppDbContext(options);

            if (_context.Database.IsRelational())
                await _context.Database.MigrateAsync();

            await _context.Database.EnsureCreatedAsync();
        }

        public async Task ResetAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }

        private void SeedData()
        {
            // Seed data
        }
    }
}

