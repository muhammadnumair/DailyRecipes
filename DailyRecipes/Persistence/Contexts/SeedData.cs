using DailyRecipes.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRecipes
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestData(
                services.GetRequiredService<RecipeApiDbContext>());
        }

        public static async Task AddTestData(RecipeApiDbContext _db)
        {
            if (!_db.Recipes.Any())
            {
                _db.Recipes.Add(new Recipe
                {

                });
            }

            if (!_db.Categories.Any())
            {
                _db.Categories.Add(new Category
                {
                    Title = "Breakfast"
                });

                _db.Categories.Add(new Category
                {
                    Title = "Salads"
                });

                _db.Categories.Add(new Category
                {
                    Title = "Appetizers"
                });
            }

            await _db.SaveChangesAsync();
        }
    }
}
