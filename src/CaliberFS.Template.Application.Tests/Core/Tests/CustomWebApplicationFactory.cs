using CaliberFS.Template.Data.Context;
using CaliberFS.Template.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CaliberFS.Template.Application.Tests.Core.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // adds a connection string to in-memory database
            var configuration = new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", "Data Source=LocalDatabase.db" }
            };

            builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.AddInMemoryCollection(configuration);
            });
                        
            // handles to connect to in-memory database
            builder.ConfigureTestServices(services =>
            {
                services
                    .RemoveAll<Domain.Interfaces.Repositories.IDbSession>()
                    .AddScoped<Domain.Interfaces.Repositories.IDbSession, Application.Tests.Core.DbConnection.DbSession>();

                services
                    .RemoveAll<Application.Services.UnitOfWork.IUnitOfWork>()
                    .AddScoped<Application.Services.UnitOfWork.IUnitOfWork, Application.Tests.Core.DbConnection.UnitOfWork>();
            });
        }
    }
}
