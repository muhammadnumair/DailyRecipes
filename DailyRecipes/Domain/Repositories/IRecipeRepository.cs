using DailyRecipes.Domain.Services.Communication;
using DailyRecipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
