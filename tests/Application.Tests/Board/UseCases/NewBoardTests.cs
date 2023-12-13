using Application.Common.Exceptions;
using Application.Tests.Core.Builders;
using Application.Tests.Core.Tests;
using static Application.Tests.Testing;

namespace Application.Tests.Board.UseCases
{
    public class NewBoardTests : BaseTest
    {
        [Test]
        public async Task ShouldCreateANewBoard()
        {
            // Arrange
            var request = BoardBuilder.BuildNewBoardRequest();

            // Act
            var response = await SendAsync(request);

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
            var request = BoardBuilder.BuildNewBoardRequest();
            await SendAsync(request);

            // Act & Assert
            await FluentActions.Invoking(() => SendAsync(request))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldNotCreateANewBoardWhenNameIsEmpty()
        {
            // Arrange
            var request = BoardBuilder.BuildNewBoardRequest();
            request.Name = string.Empty;

            // Act & Assert
            await FluentActions.Invoking(() => SendAsync(request))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldNotCreateANewBoardWhenNameIsGreaterThan50Characters()
        {
            // Arrange
            var request = BoardBuilder.BuildNewBoardRequest();
            request.Name = new string('a', 51);

            // Act & Assert
            await FluentActions.Invoking(() => SendAsync(request))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldNotCreateANewBoardWhenDescriptionIsGreaterThan500Characters()
        {
            // Arrange
            var request = BoardBuilder.BuildNewBoardRequest();
            request.Description = new string('a', 501);

            // Act & Assert
            await FluentActions.Invoking(() => SendAsync(request))
                .Should().ThrowAsync<ValidationException>();
        }
    }
}
