using DailyRecipes.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyRecipes.Domain.Models;

namespace DailyRecipes.Domain.Repositories
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetRecipes();
        Task SaveRecipe(Recipe recipe);
        Task<RecipeResponse> UpdateRecipe(Guid id, Recipe recipe);
        Recipe FindRecipeById(Guid id);
        RecipeResponse DeleteRecipe(Guid id);
    }
}
