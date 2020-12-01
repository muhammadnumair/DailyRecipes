using DailyRecipes.Domain.Services.Communication;
using DailyRecipes.Models;
using System;
using System.Collections.Generic;

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
