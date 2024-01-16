using Enterprise.GenericRepository.Interfaces;
using Enterprise.Operations;
using Enterprise.PubSub.Interfaces;
using Enterprise.Template.Application.Services.UnitOfWork;
using Enterprise.Template.Application.Tests.Core.Fakes;
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
            var genericRepositoryFactory = new Mock<IGenericRepositoryFactory>();
            var genericRepository = new Mock<IGenericRepository>();

            genericRepositoryFactory.Setup(x => x.GetRepository(It.IsAny<string>()))
                .Returns(() => genericRepository.Object);

            genericRepository.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, object>>>()))
                .ReturnsAsync(new OperationResult<int>(true, string.Empty, 1));

            genericRepository.Setup(x => x.GetSingleAsync<int>(It.IsAny<string>(), It.IsAny<System.Data.CommandType>(), It.IsAny<List<KeyValuePair<string, object>>>()))
                .ReturnsAsync(new OperationResult<int>(true, string.Empty, 0));

            genericRepository.Setup(x => x.GetAllAsync<Domain.Entities.Board>(It.IsAny<string>(), It.IsAny<System.Data.CommandType>(), It.IsAny<List<KeyValuePair<string, object>>>()))
                .ReturnsAsync(new OperationResult<List<Domain.Entities.Board>>(true, string.Empty, new List<Domain.Entities.Board>
                {
                    new Domain.Entities.Board("Schow", "Schow's board")
                }));

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
                services.AddScoped<IBoardRepository, BoardRepository>(x => new BoardRepository(genericRepositoryFactory.Object));

                services.AddSingleton<IPublisherService, PublisherServiceFake>();

                services.AddApplications();
            });
        }
    }
}
