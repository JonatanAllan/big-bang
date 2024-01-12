using Enterprise.Template.Application.Common.Exceptions;
using Enterprise.Template.Application.Interfaces;
using Enterprise.Template.Application.Tests.Core.Builders;
using Enterprise.Template.Application.Tests.Core.Tests;
using Microsoft.Extensions.DependencyInjection;
using static Enterprise.Template.Application.Tests.Testing;

namespace Enterprise.Template.Application.Tests.Board.UseCases
{
    public class NewBoardTests : BaseTest
    {
        [Test]
        public async Task ShouldCreateANewBoard()
        {
            // Arrange
            using var scope = ScopeFactory.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            var request = BoardBuilder.BuildNewBoardRequest();

            // Act
            var response = await boardApplication.CreateBoard(request);

            // Assert
            Assert.Multiple(() =>
            {
                response.Name.Should().Be(request.Name);
                response.Description.Should().Be(request.Description);
                response.Id.Should().BeGreaterThan(0);
            });
        }

        [Test]
        public async Task ShouldNotCreateANewBoardWhenNameAlreadyExists()
        {
            // Arrange
            using var scope = ScopeFactory.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            var request = BoardBuilder.BuildNewBoardRequest();
            await boardApplication.CreateBoard(request);

            // Act & Assert
            await FluentActions.Invoking(() => boardApplication.CreateBoard(request))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldNotCreateANewBoardWhenNameIsEmpty()
        {
            // Arrange
            using var scope = ScopeFactory.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            var request = BoardBuilder.BuildNewBoardRequest();
            request.Name = string.Empty;

            // Act & Assert
            await FluentActions.Invoking(() => boardApplication.CreateBoard(request))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldNotCreateANewBoardWhenNameIsGreaterThan50Characters()
        {
            // Arrange
            using var scope = ScopeFactory.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            var request = BoardBuilder.BuildNewBoardRequest();
            request.Name = new string('a', 51);

            // Act & Assert
            await FluentActions.Invoking(() => boardApplication.CreateBoard(request))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldNotCreateANewBoardWhenDescriptionIsGreaterThan500Characters()
        {
            // Arrange
            using var scope = ScopeFactory.CreateScope();
            var boardApplication = scope.ServiceProvider.GetRequiredService<IBoardApplication>();
            var request = BoardBuilder.BuildNewBoardRequest();
            request.Description = new string('a', 501);

            // Act & Assert
            await FluentActions.Invoking(() => boardApplication.CreateBoard(request))
                .Should().ThrowAsync<ValidationException>();
        }
    }
}
