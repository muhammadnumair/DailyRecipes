using DailyRecipes.Domain.Services.Communication;
using DailyRecipes.Models;
using System;
using System.Collections.Generic;
using DailyRecipes.Domain.Models;

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
