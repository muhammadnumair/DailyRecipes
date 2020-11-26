using DailyRecipes.Domain.Services.Communication;
using DailyRecipes.Models;
using DailyRecipes.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRecipes.Domain.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        CategoryResponse SaveCategory(Category category);
        CategoryResponse UpdateCategory(Guid id, Category category);
        CategoryResponse DeleteCategory(Guid id);
    }
}
