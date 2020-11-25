using DailyRecipes.Domain.Repositories;
using DailyRecipes.Domain.Services;
using DailyRecipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRecipes.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }
    }
}
