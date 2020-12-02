using DailyRecipes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DailyRecipes.Domain.Models;

namespace DailyRecipes.Resources
{
    public class SaveRecipeResource
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(130)]
        public string Excerpt { get; set; }
        public string Instructions { get; set; }
        public string Type { get; set; }
        public string Calories { get; set; }
        public string Time { get; set; }
        public List<Resource> Needed { get; set; } = new List<Resource>();
        public List<Ingredient> Ingredient { get; set; } = new List<Ingredient>();
        public string VideoUrl { get; set; }
        public byte[] Thumbnail { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
