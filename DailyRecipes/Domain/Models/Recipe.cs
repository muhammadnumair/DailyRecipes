using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRecipes.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Description { get; set; }
    }
}
