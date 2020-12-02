using Microsoft.EntityFrameworkCore;
using DailyRecipes.Domain.Models;

namespace DailyRecipes
{
    public class RecipeApiDbContext: DbContext
    {
        public RecipeApiDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
