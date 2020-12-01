using DailyRecipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRecipes.Tests.Resources
{
    public class RecipeResource
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Instructions { get; set; }
        public string Type { get; set; }
        public string Calories { get; set; }
        public string Time { get; set; }
        public List<Resource> Needed { get; set; } = new List<Resource>();
        public List<Ingredient> Ingredient { get; set; } = new List<Ingredient>();
        public string VideoUrl { get; set; }
        public Category Category { get; set; }
        public byte[] Thumbnail { get; set; }
    }
}
