using DailyRecipes.Domain.Repositories;
using DailyRecipes.Domain.Services.Communication;
using DailyRecipes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRecipes.Persistence.Repositories
{
    public class RecipeRepository : BaseRepository, IRecipeRepository
    {
        public RecipeRepository(RecipeApiDbContext context) : base(context)
        {
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            var result = _context.Recipes.Include(p => p.Category).Include(p => p.Needed).Include(p => p.Ingredient) as IQueryable<Recipe>;
            return result.OrderBy(c => c.Title).AsEnumerable<Recipe>();
        }

        public async Task SaveRecipe(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<RecipeResponse> UpdateRecipe(Guid id, Recipe recipe)
        {
            var existingRecipe = FindRecipeById(id);
            if (existingRecipe == null)
                return new RecipeResponse("Recipe not found.");

            existingRecipe.Title = recipe.Title;
            existingRecipe.Excerpt = recipe.Excerpt;
            _context.Recipes.Update(existingRecipe);
            await _context.SaveChangesAsync();
            return new RecipeResponse(existingRecipe);
        }

        public RecipeResponse DeleteRecipe(Guid id)
        {
            var existingRecipe = FindRecipeById(id);
            if (existingRecipe == null)
                return new RecipeResponse("Recipe not found.");

            _context.Recipes.Remove(existingRecipe);
            _context.SaveChangesAsync();
            return new RecipeResponse(existingRecipe);
        }

        public Recipe FindRecipeById(Guid id)
        {
            return _context.Recipes.Find(id);
        }
    }
}
