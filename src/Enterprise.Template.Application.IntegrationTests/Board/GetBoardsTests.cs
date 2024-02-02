using Enterprise.Template.Application.Common.Exceptions;
using Enterprise.Template.Application.IntegrationTests.Core.Tests;
using Enterprise.Template.Application.Interfaces;
using Enterprise.Template.Application.Models.Boards;
using Enterprise.Template.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using static Enterprise.Template.Application.IntegrationTests.Testing;

namespace Enterprise.Template.Application.IntegrationTests.Board
{
    public class GetBoardsTests : BaseTest
    {
        [Test]
        public async Task ShouldGetBoards()
        {
            // Arrange
            using var scope = ScopeFactory.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            var request = new GetBoardsRequest();

            // Act
            var response = await boardApplication.GetBoards(request);

            // Assert
            Assert.Multiple(() =>
            {
                response.Data.Count.Should().BeGreaterThan(0);
                response.Data.Count.Should().BeLessOrEqualTo(25);
                response.Total.Should().BeGreaterThan(0);
            });
        }

        [Test]
        public async Task ShouldGetBoardsWhichNameContains()
        {
            // Arrange
            using var scope = ScopeFactory.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            var boardRepository = scope.ServiceProvider.GetRequiredService<IBoardRepository>();
            var sample = await boardRepository.GetManyAsync(string.Empty, 0, 10);

            var searchName = sample.FirstOrDefault().Name;
            var request = new GetBoardsRequest
            {
                Name = searchName
            };

            // Act
            var response = await boardApplication.GetBoards(request);

            // Assert
            Assert.Multiple(() =>
            {
                response.Data.Count.Should().BeGreaterThan(0);
                response.Data.Count.Should().BeLessOrEqualTo(25);
                response.Total.Should().BeGreaterThan(0);
                response.Data.Should().AllSatisfy(s => s.Name.Contains(searchName));
            });
        }

        [Test]
        public async Task ShouldFailOnGetBoardsWhenNameIsInvalid()
        {
            // Arrange
            using var scope = ScopeFactory.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            var request = new GetBoardsRequest
            {
                Name = "a"
            };

            // Act & Assert
            await FluentActions.Invoking(() => boardApplication.GetBoards(request))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldFailOnGetBoardsWhenSkipIsInvalid()
        {
            // Arrange
            using var scope = ScopeFactory.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            var request = new GetBoardsRequest
            {
                Skip = -1
            };

            // Act & Assert
            await FluentActions.Invoking(() => boardApplication.GetBoards(request))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldFailOnGetBoardsWhenTakeIsInvalid()
        {
            // Arrange
            using var scope = ScopeFactory.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            var request = new GetBoardsRequest
            {
                Take = 0
            };

            // Act & Assert
            await FluentActions.Invoking(() => boardApplication.GetBoards(request))
                .Should().ThrowAsync<ValidationException>();
        }
    }
}
