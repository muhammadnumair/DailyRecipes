using DailyRecipes.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyRecipes.Tests.Models
{
    public class Recipe
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
        [Newtonsoft.Json.JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] Thumbnail { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class Ingredient
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string Description { get; set; }
    }
}
