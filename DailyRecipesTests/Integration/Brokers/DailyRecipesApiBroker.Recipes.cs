using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DailyRecipes.Tests.Models;
using DailyRecipes.Tests.Resources;
using System.Net.Http;
namespace DailyRecipes.Tests.Integration.Brokers
{
    public partial class DailyRecipesApiBroker
    {
        private const string RecipesRelativeUrl = "api/recipes";

        public Task<HttpResponseMessage> PostRecipeAsync(HttpContent body)
        {
            var response = baseClient.PostAsync(RecipesRelativeUrl, body);
            return response;
        }

        public Task<HttpResponseMessage> GetRecipeAsync()
        {
            var response = baseClient.GetAsync(RecipesRelativeUrl);
            return response;
        }

        public async ValueTask<Recipe> DeleteRecipeAsync(Guid recipeId) =>
            await apiFactoryClient.DeleteContentAsync<Recipe>($"{RecipesRelativeUrl}/{recipeId}");
    }
}
