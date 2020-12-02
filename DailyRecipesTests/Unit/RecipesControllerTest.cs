using System.Collections.Generic;
using AutoMapper;
using DailyRecipes.Controllers;
using DailyRecipes.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;
using System.Net;
using DailyRecipes.Domain.Models;
using DailyRecipes.Domain.Services.Communication;
using DailyRecipes.Resources;
using Tynamix.ObjectFiller;

namespace DailyRecipes.Tests.Unit
{
    public class RecipesControllerTest
    {
        private readonly Mock<ILogger<Program>> _logger;
        private readonly MapperConfiguration _mappingConfig;
        private readonly Mock<IRecipeService> _mockService;


        private static SaveRecipeResource CreateRandomRecipe() =>
            new Filler<SaveRecipeResource>().Create();

        private static Recipe CreateRandomRecipe2() =>
            new Filler<Recipe>().Create();

        private RecipesController _recipesController;

        public RecipesControllerTest()
        {
            _mockService = new Mock<IRecipeService>();
            var recipeResponse = new RecipeResponse(CreateRandomRecipe2());
            _mockService.Setup(s => s.GetRecipes()).Returns(new List<Recipe>());
            _mockService.Setup(s => s.SaveRecipe(It.IsAny<Recipe>()))
                .Returns(recipeResponse);
            _mockService.Setup(s => s.UpdateRecipe(It.IsAny<Guid>(), It.IsAny<Recipe>()))
                .Returns(recipeResponse);
            _mockService.Setup(s => s.DeleteRecipe(It.IsAny<Guid>())).Returns(recipeResponse);

            _logger = new Mock<ILogger<Program>>();
            _mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new Mapping.ModelToResourceProfile()); });

            _recipesController = new RecipesController(_mockService.Object, _logger.Object, _mappingConfig.CreateMapper());
        }

        [Fact]
        public void GetRecipes_Success()
        {
            var result = _recipesController.GetRecipes();
            result.Should().NotBeNull();
            try
            {
                var res = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
                res.StatusCode.Should().Be((int)HttpStatusCode.OK);
            }
            catch (Exception)
            {
                var res = (Microsoft.AspNetCore.Mvc.NoContentResult)result;
                res.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            }
        }

        [Fact]
        public void SaveRecipe_Success()
        {
            var result = _recipesController.SaveRecipe(CreateRandomRecipe());
            result.Should().NotBeNull();
            var res = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            res.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void UpdateRecipe_Success()
        {
            var result = _recipesController.UpdateRecipe(new Guid(), CreateRandomRecipe());
            var res = (Microsoft.AspNetCore.Mvc.OkObjectResult) result;
            res.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Should().NotBeNull();
        }

        [Fact]
        public void UpdateRecipe_Failure()
        {
            _mockService.Setup(s => s.UpdateRecipe(It.IsAny<Guid>(), It.IsAny<Recipe>()))
                .Returns(new RecipeResponse("Bad Request"));
            _recipesController = new RecipesController(_mockService.Object, _logger.Object, _mappingConfig.CreateMapper());
            var result = _recipesController.UpdateRecipe(new Guid(), CreateRandomRecipe());
            result.Should().NotBeNull();
            var res = (Microsoft.AspNetCore.Mvc.NotFoundResult)result;
            res.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public void SaveRecipe_Failure()
        {
            var recipeResponse = new RecipeResponse("Null Category");
            _mockService.Setup(s => s.SaveRecipe(It.IsAny<Recipe>()))
                .Returns(recipeResponse);
            _recipesController = new RecipesController(_mockService.Object, _logger.Object, _mappingConfig.CreateMapper());
            var result = _recipesController.SaveRecipe(CreateRandomRecipe());
            result.Should().NotBeNull();
            var res = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            res.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public void DeleteRecipe_Success()
        {
            var result = _recipesController.DeleteRecipe(new Guid());
            var res = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            res.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Should().NotBeNull();
        }

        [Fact]
        public void DeleteRecipe_Failure()
        {
            _mockService.Setup(s => s.DeleteRecipe(It.IsAny<Guid>()))
                .Returns(new RecipeResponse("Bad Request"));
            _recipesController = new RecipesController(_mockService.Object, _logger.Object, _mappingConfig.CreateMapper());
            var result = _recipesController.DeleteRecipe(new Guid());
            result.Should().NotBeNull();
            var res = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            res.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
