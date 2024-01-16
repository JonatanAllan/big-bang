using Enterprise.Template.Application.Common.Exceptions;
using Enterprise.Template.Application.Interfaces;
using Enterprise.Template.Application.Models.Boards;
using Enterprise.Template.Application.Tests.Core.Tests;
using Microsoft.Extensions.DependencyInjection;
using static Enterprise.Template.Application.Tests.Testing;

namespace Enterprise.Template.Application.Tests.Board.UseCases
{
    public class GetBoardsTests : BaseTest
    {
        [Test]
        public async Task ShouldGetBoardsWhichNameContains()
        {
            // Arrange
            using var scope = ScopeFactory.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            
            var request = new GetBoardsRequest
            {
                Name = "Schow"
            };

            // Act
            var response = await boardApplication.GetBoards(request);

            // Assert
            Assert.Multiple(() =>
            {
                response.Data.Count.Should().Be(1);
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
