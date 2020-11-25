using DailyRecipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRecipes.Domain.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
    }
}
