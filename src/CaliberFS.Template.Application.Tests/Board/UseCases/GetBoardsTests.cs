using CaliberFS.Template.Application.Common.Exceptions;
using CaliberFS.Template.Application.Tests.Core.Builders;
using CaliberFS.Template.Application.Tests.Core.Tests;
using CaliberFS.Template.Application.UseCases.GetBoards;
using static CaliberFS.Template.Application.Tests.Testing;

namespace CaliberFS.Template.Application.Tests.Board.UseCases
{
    public class GetBoardsTests : BaseTest
    {
        [Test]
        public async Task ShouldGetBoardsWithDefaultPagination()
        {
            // Arrange
            var boards = BoardBuilder.ManyNewBoardEntity(30);
            await AddManyAsync(boards);
            var request = new GetBoardsRequest();

            // Act
            var response = await SendAsync(request);

            // Assert
            Assert.Multiple(() =>
            {
                response.Data.Count.Should().Be(request.Take);
                response.Total.Should().Be(30);
            });
        }

        [Test]
        public async Task ShouldGetBoardsWithPagination()
        {
            // Arrange
            var boards = BoardBuilder.ManyNewBoardEntity(30);
            await AddManyAsync(boards);
            
            var request = new GetBoardsRequest
            {
                Skip = 10,
                Take = 15
            };

            // Act
            var response = await SendAsync(request);

            // Assert
            var expected = boards.Skip(10).Take(15).ToList();
            Assert.Multiple(() =>
            {
                response.Data.Count.Should().Be(15);
                response.Total.Should().Be(30);
                Assert.That(expected.Select(x => x.Id), Is.EquivalentTo(response.Data.Select(x => x.Id)));
            });
        }

        [Test]
        public async Task ShouldGetBoardsWhichNameContains()
        {
            // Arrange
            var boards = BoardBuilder.ManyNewBoardEntity(30);
            await AddManyAsync(boards);
            var name = boards.First(x => x.Name.Length >= 5).Name;
            var request = new GetBoardsRequest
            {
                Name = name[..5]
            };

            // Act
            var response = await SendAsync(request);

            // Assert
            var expected = boards.Where(x => x.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase)).ToList();
            Assert.Multiple(() =>
            {
                response.Data.Count.Should().Be(expected.Count);
                response.Total.Should().Be(expected.Count);
                Assert.That(expected.Select(x => x.Id), Is.EquivalentTo(response.Data.Select(x => x.Id)));
            });
        }

        [Test]
        public async Task ShouldFailOnGetBoardsWhenNameIsInvalid()
        {
            // Arrange
            var request = new GetBoardsRequest
            {
                Name = "a"
            };

            // Act & Assert
            await FluentActions.Invoking(() => SendAsync(request))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldFailOnGetBoardsWhenSkipIsInvalid()
        {
            // Arrange
            var request = new GetBoardsRequest
            {
                Skip = -1
            };

            // Act & Assert
            await FluentActions.Invoking(() => SendAsync(request))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldFailOnGetBoardsWhenTakeIsInvalid()
        {
            // Arrange
            var request = new GetBoardsRequest
            {
                Take = 0
            };

            // Act & Assert
            await FluentActions.Invoking(() => SendAsync(request))
                .Should().ThrowAsync<ValidationException>();
        }
    }
}
