using AutoMapper;
using DailyRecipes.Domain.Repositories;
using DailyRecipes.Domain.Services;
using DailyRecipes.Domain.Services.Communication;
using DailyRecipes.Models;
using System;
using System.Collections.Generic;
using DailyRecipes.Domain.Models;

namespace DailyRecipes.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public CategoryResponse SaveCategory(Category category)
        {
            _categoryRepository.SaveCategory(category);
            return new CategoryResponse(category);
        }

        public CategoryResponse UpdateCategory(Guid id, Category category)
        {
            _categoryRepository.UpdateCategory(id, category);
            return new CategoryResponse(category);
        }

        public CategoryResponse DeleteCategory(Guid id)
        {
            return _categoryRepository.DeleteCategory(id);
        }
    }
}
