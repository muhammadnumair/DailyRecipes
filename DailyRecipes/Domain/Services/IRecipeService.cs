using DailyRecipes.Domain.Services.Communication;
using DailyRecipes.Models;
using System;
using System.Collections.Generic;

namespace DailyRecipes.Domain.Services
{
    public interface IRecipeService
    {
        IEnumerable<Recipe> GetRecipes();
        RecipeResponse SaveRecipe(Recipe recipe);
        RecipeResponse UpdateRecipe(Guid id, Recipe recipe);
        RecipeResponse DeleteRecipe(Guid id);
    }
}
