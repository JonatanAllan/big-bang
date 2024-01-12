using Enterprise.Template.Application.Services.RabbitMQ;
using Enterprise.Template.Application.Services.UnitOfWork;
using Enterprise.Template.Application.Tests.Core.Fakes;
using Enterprise.Template.Core.RabbitMQ.Producer;
using Enterprise.Template.Data.Context;
using Enterprise.Template.Data.Repositories;
using Enterprise.Template.Data.UnitOfWork;
using Enterprise.Template.Domain.Interfaces.Repositories;
using Enterprise.Template.IoC.DependencyInjection;
using Enterprise.Template.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Enterprise.Template.Application.Tests.Core.Tests
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

                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<IBoardRepository, BoardRepository>();

                services.AddSingleton<IRabbitMqProducer<SampleIntegrationEvent>, SampleProducerFake>();

                services.AddApplications();
            });
        }
    }
}
