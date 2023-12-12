using Application.Tests.Core.Builders;
using Application.Tests.Core.Fixture;
using Application.UseCases.NewBoard;
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
                .Should().ThrowAsync<Exception>().WithMessage("Board already exists");
        }
    }
}
