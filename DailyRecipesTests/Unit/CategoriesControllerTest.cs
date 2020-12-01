using System;
using System.Collections.Generic;
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

        private Category CreateRandomCategory2() =>
            new Filler<Category>().Create();

        private readonly CategoriesController _categoriesController;
        public CategoriesControllerTest()
        {
            var mockService = new Mock<ICategoryService>();
            var categoryResponse = new CategoryResponse(CreateRandomCategory2());
            mockService.Setup(s => s.GetCategories()).Returns(new List<Category>());
            mockService.Setup(s => s.SaveCategory(It.IsAny<Category>()))
                .Returns(categoryResponse);
            mockService.Setup(s => s.UpdateCategory(It.IsAny<Guid>(), It.IsAny<Category>()))
                .Returns(categoryResponse);
            mockService.Setup(s => s.DeleteCategory(It.IsAny<Guid>())).Returns(categoryResponse);

            var logger = new Mock<ILogger<Program>>();
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new Mapping.ModelToResourceProfile());
            });

            var mockMapper = mappingConfig.CreateMapper();

            _categoriesController = new CategoriesController(mockService.Object, logger.Object, mockMapper);
        }

        [Fact]
        public void GetCategories_Success()
        {
            var result = _categoriesController.GetCategories();
            result.Should().NotBeNull();
        }

        [Fact]
        public void SaveCategory_Success()
        {
            var result = _categoriesController.SaveCategory(CreateRandomCategory());
            result.Should().NotBeNull();
        }

        [Fact]
        public void UpdateCategory_Success()
        {
            var result = _categoriesController.UpdateCategory(new Guid(), CreateRandomCategory());
            result.Should().NotBeNull();
        }

        [Fact]
        public void DeleteCategory_Success()
        {
            var result = _categoriesController.DeleteCategory(new Guid());
            result.Should().NotBeNull();
        }
    }
}
