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
        private const string CatgeoriesRelativeUrl = "api/categories";

        public Task<HttpResponseMessage> PostAsync(HttpContent body)
        {
            var response = this.baseClient.PostAsync(CatgeoriesRelativeUrl, body);
            return response;
        }

        public Task<HttpResponseMessage> GetAsync()
        {
            var response = this.baseClient.GetAsync(CatgeoriesRelativeUrl);
            return response;
        }

        public async ValueTask<Category> DeleteAsync(Guid categoryId) =>
            await this.apiFactoryClient.DeleteContentAsync<Category>($"{CatgeoriesRelativeUrl}/{categoryId}");
    }
}
