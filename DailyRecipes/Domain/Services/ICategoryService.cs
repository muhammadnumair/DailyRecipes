using DailyRecipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRecipes.Domain.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
