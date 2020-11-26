using DailyRecipes.Domain.Services.Communication;
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
        Task SaveCategory(Category category);
        Task<CategoryResponse> UpdateCategory(Guid id, Category category);
        Category FindCategoryById(Guid id);
        CategoryResponse DeleteCategory(Guid id);
    }
}
