using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using DailyRecipes.Controllers;
using DailyRecipes.Domain.Models;
using DailyRecipes.Domain.Services;
using DailyRecipes.Domain.Services.Communication;
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
        private static SaveCategoryResource CreateRandomCategory() =>
            new Filler<SaveCategoryResource>().Create();

        private static Category CreateRandomCategory2() =>
            new Filler<Category>().Create();

        private readonly Mock<ILogger<Program>> _logger;
        private readonly MapperConfiguration _mappingConfig;
        private readonly Mock<ICategoryService> _mockService;


        private CategoriesController _categoriesController;
        public CategoriesControllerTest()
        {
            _mockService = new Mock<ICategoryService>();
            var categoryResponse = new CategoryResponse(CreateRandomCategory2());
            _mockService.Setup(s => s.GetCategories()).Returns(new List<Category>());
            _mockService.Setup(s => s.SaveCategory(It.IsAny<Category>()))
                .Returns(categoryResponse);
            _mockService.Setup(s => s.UpdateCategory(It.IsAny<Guid>(), It.IsAny<Category>()))
                .Returns(categoryResponse);
            _mockService.Setup(s => s.DeleteCategory(It.IsAny<Guid>())).Returns(categoryResponse);

            _logger = new Mock<ILogger<Program>>();
            _mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new Mapping.ModelToResourceProfile());
            });
            _categoriesController = new CategoriesController(_mockService.Object, _logger.Object, _mappingConfig.CreateMapper());
        }


        [Fact]
        public void SaveCategory_Success()
        {
            var result = _categoriesController.SaveCategory(CreateRandomCategory());
            result.Should().NotBeNull();
            var res = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            res.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void SaveCategory_Failure()
        {
            var categoryResponse = new CategoryResponse("Null Category");
            _mockService.Setup(s => s.SaveCategory(It.IsAny<Category>()))
                .Returns(categoryResponse);
            _categoriesController = new CategoriesController(_mockService.Object, _logger.Object, _mappingConfig.CreateMapper());
            var result = _categoriesController.SaveCategory(CreateRandomCategory());
            result.Should().NotBeNull();
            var res = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            res.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public void UpdateCategory_Success()
        {
            var result = _categoriesController.UpdateCategory(new Guid(), CreateRandomCategory());
            result.Should().NotBeNull();
            var res = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            res.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void UpdateCategory_Failure()
        {
            _mockService.Setup(s => s.UpdateCategory(It.IsAny<Guid>(), It.IsAny<Category>()))
                .Returns(new CategoryResponse("Bad Request"));
            _categoriesController = new CategoriesController(_mockService.Object, _logger.Object, _mappingConfig.CreateMapper());
            var result = _categoriesController.UpdateCategory(new Guid(), CreateRandomCategory());
            result.Should().NotBeNull();
            var res = (Microsoft.AspNetCore.Mvc.NotFoundResult)result;
            res.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public void GetCategories_Success()
        {
            var result = _categoriesController.GetCategories();
            result.Should().NotBeNull();
            try
            {
                var res = (Microsoft.AspNetCore.Mvc.OkObjectResult) result;
                res.StatusCode.Should().Be((int) HttpStatusCode.OK);
            }
            catch (Exception)
            {
                var res = (Microsoft.AspNetCore.Mvc.NoContentResult)result;
                res.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            }
        }

        [Fact]
        public void DeleteCategory_Success()
        {
            var result = _categoriesController.DeleteCategory(new Guid());
            result.Should().NotBeNull();
            var res = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            res.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void DeleteCategory_Failure()
        {
            _mockService.Setup(s => s.DeleteCategory(It.IsAny<Guid>()))
                .Returns(new CategoryResponse("Bad Request"));
            _categoriesController = new CategoriesController(_mockService.Object, _logger.Object, _mappingConfig.CreateMapper());
            var result = _categoriesController.DeleteCategory(new Guid());
            result.Should().NotBeNull();
            var res = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            res.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
