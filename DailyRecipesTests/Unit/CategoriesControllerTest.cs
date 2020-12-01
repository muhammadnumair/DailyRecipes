using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DailyRecipes.Controllers;
using DailyRecipes.Domain.Services;
using DailyRecipes.Domain.Services.Communication;
using DailyRecipes.Models;
using DailyRecipes.Resources;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Tynamix.ObjectFiller;
using Xunit;

namespace DailyRecipes.Tests.Unit
{
    public class CategoriesControllerTest
    {
        private SaveCategoryResource CreateRandomCategory() =>
            new Filler<SaveCategoryResource>().Create();

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

        [Fact]
        public void SaveCategory_Success()
        {
            // Arrange
            var mockService = new Mock<ICategoryService>();
            var category = new Mock<Category>();
            mockService.Setup(s => s.SaveCategory(new Category()))
                .Returns(new CategoryResponse(new Category()));

            var logger = new Mock<ILogger<Program>>();
            var mockMapper = new Mock<IMapper>();

            //IEnumerable<Category>
            var controller = new CategoriesController(mockService.Object, logger.Object, mockMapper.Object);

            // Act
            var result = controller.SaveCategory(CreateRandomCategory());

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UpdateCategory_Success()
        {
            // Arrange
            var mockService = new Mock<ICategoryService>();
            var category = new Mock<Category>();
            mockService.Setup(s => s.UpdateCategory(new Guid(), category.Object))
                .Returns(new DailyRecipes.Domain.Services.Communication.CategoryResponse(new Category()));

            var logger = new Mock<ILogger<Program>>();
            var mockMapper = new Mock<IMapper>();

            //IEnumerable<Category>
            var controller = new CategoriesController(mockService.Object, logger.Object, mockMapper.Object);

            // Act
            var mockModel = new Mock<SaveCategoryResource>();
            var result = controller.UpdateCategory(new Guid(), mockModel.Object);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(Task.CompletedTask);
        }
    }
}
