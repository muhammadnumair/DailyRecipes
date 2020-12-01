using System.Collections.Generic;
using AutoMapper;
using DailyRecipes.Controllers;
using DailyRecipes.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DailyRecipes.Tests.Unit
{
    public class RecipesControllerTest
    {
        [Fact]
        public void GetCategories_Success()
        {
            // Arrange
            var mockService = new Mock<ICategoryService>();
            mockService.Setup(s => s.GetCategories()).Returns(new List<DailyRecipes.Models.Category>());

            var logger = new Mock<ILogger<Program>>();
            var mockMapper = new Mock<IMapper>();

            //IEnumerable<Category>
            var controller = new CategoriesController(mockService.Object, logger.Object, mockMapper.Object);

            // Act
            var result = controller.GetCategories();

            // Assert
            result.Should().NotBeNull();
        }
    }
}
