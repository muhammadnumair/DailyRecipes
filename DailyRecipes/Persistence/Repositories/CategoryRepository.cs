using DailyRecipes.Domain.Repositories;
using DailyRecipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRecipes.Persistence.Repositories
{
    public class CategoryRepository: BaseRepository, ICategoryRepository
    {
        public CategoryRepository(RecipeApiDbContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetCategories()
        {
            var result = _context.Categories as IQueryable<Category>;
            return result.OrderBy(c => c.Title).AsEnumerable<Category>();
        }
    }
}
