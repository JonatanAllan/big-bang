using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Template.Application.Models.Boards;
using FluentAssertions;

namespace Enterprise.Template.Application.UnitTests.Board
{
    public class BoardValidationTests
    {
        [Test]
        public async Task ShouldReturnInvalidWhenNameIsEmpty()
        {
            // Arrange
            var request = new NewBoardRequest
            {
                Name = string.Empty,
                Description = "Description"
            };

            // Act
            var result = await request.Validate();

            // Assert
            Assert.Multiple(() =>
            {
                result.IsValid.Should().BeFalse();
                result.Errors.Should().Contain(e => e.PropertyName == "Name");
            });
        }

        [Test]
        public async Task ShouldReturnInvalidWhenNameIsGreaterThan50Characters()
        {
            // Arrange
            var request = new NewBoardRequest
            {
                Name = new string('a', 51),
                Description = "Description"
            };

            // Act
            var result = await request.Validate();

            // Assert
            Assert.Multiple(() =>
            {
                result.IsValid.Should().BeFalse();
                result.Errors.Should().Contain(e => e.PropertyName == "Name");
            });
        }

        [Test]
        public async Task ShouldReturnInvalidWhenDescriptionIsGreaterThan500Characters()
        {
            // Arrange
            var request = new NewBoardRequest
            {
                Name = "Name",
                Description = new string('a', 501)
            };

            // Act
            var result = await request.Validate();

            // Assert
            Assert.Multiple(() =>
            {
                result.IsValid.Should().BeFalse();
                result.Errors.Should().Contain(e => e.PropertyName == "Description");
            });
        }

        [Test]
        public async Task ShouldReturnValidWhenNameAndDescriptionAreValid()
        {
            // Arrange
            var request = new NewBoardRequest
            {
                Name = "Name",
                Description = "Description"
            };

            // Act
            var result = await request.Validate();

            // Assert
            Assert.Multiple(() =>
            {
                result.IsValid.Should().BeTrue();
                result.Errors.Should().BeEmpty();
            });
        }
    }
}
