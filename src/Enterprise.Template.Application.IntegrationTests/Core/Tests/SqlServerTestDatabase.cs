namespace Enterprise.Template.Application.IntegrationTests.Core.Tests
{
    public class SqlServerTestDatabase : ITestDatabase
    {
        private const string DatabaseName = "Enterprise";

        public async Task InitialiseAsync()
        {
            // Todo: Initialize database
        }

        public async Task ResetAsync()
        {
            // Todo: Clear database
        }

        private void SeedData()
        {
            // Seed data
        }
    }
}

