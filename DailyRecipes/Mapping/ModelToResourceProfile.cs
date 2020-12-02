using AutoMapper;
using DailyRecipes.Resources;
using DailyRecipes.Domain.Models;

namespace DailyRecipes.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            // Categories Models Mapping
            CreateMap<Category, CategoryResource>();
            CreateMap<SaveCategoryResource, Category>();
            CreateMap<Category, CategoryResource>();


            // Categories Models Mapping
            CreateMap<Recipe, RecipeResource>();
            CreateMap<SaveRecipeResource, Recipe>();
            CreateMap<Recipe, RecipeResource>();
        }
    }
}
