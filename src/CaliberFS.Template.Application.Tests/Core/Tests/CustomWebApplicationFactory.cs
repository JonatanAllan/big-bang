using CaliberFS.Template.Data.Context;
using CaliberFS.Template.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CaliberFS.Template.Application.Tests.Core.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services
                    .RemoveAll<DbContextOptions<AppDbContext>>()
                    .AddDbContext<AppDbContext>((sp, options) =>
                    {
                        options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                        options.UseInMemoryDatabase("sample");
                    });
            });
        }
    }
}
