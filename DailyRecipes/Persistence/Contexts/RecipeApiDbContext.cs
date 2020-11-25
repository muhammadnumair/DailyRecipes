using DailyRecipes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
