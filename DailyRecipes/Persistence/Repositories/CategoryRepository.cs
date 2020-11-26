using DailyRecipes.Domain.Repositories;
using DailyRecipes.Domain.Services.Communication;
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

        public async Task SaveCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryResponse> UpdateCategory(Guid id, Category category)
        {
            var existingCategory = FindCategoryById(id);
            if(existingCategory == null)
                return new CategoryResponse("Category not found.");

            existingCategory.Title = category.Title;
            existingCategory.Excerpt = category.Excerpt;
            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();
            return new CategoryResponse(existingCategory);
        }

        public CategoryResponse DeleteCategory(Guid id)
        {
            var existingCategory = FindCategoryById(id);
            if (existingCategory == null)
                return new CategoryResponse("Category not found.");

            _context.Categories.Remove(existingCategory);
            _context.SaveChangesAsync();
            return new CategoryResponse(existingCategory);
        }

        public Category FindCategoryById(Guid id)
        {
            return _context.Categories.Find(id);
        }
    }
}
