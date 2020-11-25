using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRecipes.Persistence.Repositories
{
    public class BaseRepository
    {
        protected readonly RecipeApiDbContext _context;
        public BaseRepository(RecipeApiDbContext context)
        {
            _context = context;
        }
    }
}
