using AutoMapper;
using DailyRecipes.Domain.Repositories;
using DailyRecipes.Domain.Services;
using DailyRecipes.Domain.Services.Communication;
using DailyRecipes.Models;
using System;
using System.Collections.Generic;
using DailyRecipes.Domain.Models;

namespace DailyRecipes.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            return _recipeRepository.GetRecipes();
        }

        public RecipeResponse SaveRecipe(Recipe Recipe)
        {
            _recipeRepository.SaveRecipe(Recipe);
            return new RecipeResponse(Recipe);
        }

        public RecipeResponse UpdateRecipe(Guid id, Recipe Recipe)
        {
            _recipeRepository.UpdateRecipe(id, Recipe);
            return new RecipeResponse(Recipe);
        }

        public RecipeResponse DeleteRecipe(Guid id)
        {
            return _recipeRepository.DeleteRecipe(id);
        }
    }
}
