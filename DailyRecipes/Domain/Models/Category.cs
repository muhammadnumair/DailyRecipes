﻿using System;

namespace DailyRecipes.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
    }
}
