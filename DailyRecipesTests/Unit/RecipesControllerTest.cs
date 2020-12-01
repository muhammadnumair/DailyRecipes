using System.Collections.Generic;
using AutoMapper;
using DailyRecipes.Controllers;
using DailyRecipes.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;
using DailyRecipes.Domain.Services.Communication;
using DailyRecipes.Models;
using DailyRecipes.Resources;
using Tynamix.ObjectFiller;

namespace DailyRecipes.Tests.Unit
{
    public class RecipesControllerTest
    {
        private SaveRecipeResource CreateRandomRecipe() =>
            new Filler<SaveRecipeResource>().Create();

        private Recipe CreateRandomRecipe2() =>
            new Filler<Recipe>().Create();

        private readonly RecipesController _recipesController;

        public RecipesControllerTest()
        {
            var mockService = new Mock<IRecipeService>();
            var recipeResponse = new RecipeResponse(CreateRandomRecipe2());
            mockService.Setup(s => s.GetRecipes()).Returns(new List<Recipe>());
            mockService.Setup(s => s.SaveRecipe(It.IsAny<Recipe>()))
                .Returns(recipeResponse);
            mockService.Setup(s => s.UpdateRecipe(It.IsAny<Guid>(), It.IsAny<Recipe>()))
                .Returns(recipeResponse);
            mockService.Setup(s => s.DeleteRecipe(It.IsAny<Guid>())).Returns(recipeResponse);

            var logger = new Mock<ILogger<Program>>();
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new Mapping.ModelToResourceProfile()); });

            var mockMapper = mappingConfig.CreateMapper();

            _recipesController = new RecipesController(mockService.Object, logger.Object, mockMapper);
        }

        [Fact]
        public void GetCategories_Success()
        {
            var result = _recipesController.GetRecipes();
            result.Should().NotBeNull();
        }

        [Fact]
        public void SaveCategory_Success()
        {
            var result = _recipesController.SaveRecipe(CreateRandomRecipe());
            result.Should().NotBeNull();
        }

        [Fact]
        public void UpdateCategory_Success()
        {
            var result = _recipesController.UpdateRecipe(new Guid(), CreateRandomRecipe());
            result.Should().NotBeNull();
        }

        [Fact]
        public void DeleteCategory_Success()
        {
            var result = _recipesController.DeleteRecipe(new Guid());
            result.Should().NotBeNull();
        }
    }
}
