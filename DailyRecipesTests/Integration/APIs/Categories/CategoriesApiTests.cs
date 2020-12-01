using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using DailyRecipes.Tests.Models;
using Tynamix.ObjectFiller;
using FluentAssertions;
using DailyRecipes.Tests.Resources;
using System.Net;
using Newtonsoft.Json;

namespace DailyRecipes.Tests.Integration.APIs.Categories
{
    [Collection(nameof(Brokers.ApiTestCollection))]
    public class CategoriesApiTests
    {
        private readonly Brokers.DailyRecipesApiBroker dailyRecipesApiBroker;
        public CategoriesApiTests(Brokers.DailyRecipesApiBroker dailyRecipesApiBroker) =>
            this.dailyRecipesApiBroker = dailyRecipesApiBroker;

        private SaveCategoryResource CreateRandomCategory() =>
            new Filler<SaveCategoryResource>().Create();

        [Fact]
        public async System.Threading.Tasks.Task PostCategoriesAsync_Success()
        {
            // given
            var randomCategory = CreateRandomCategory();

            var inputCategory = randomCategory;
            var json = JsonConvert.SerializeObject(inputCategory, Formatting.Indented);
            var stringContent = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            var expectedCategory = inputCategory;

            // when
            var responseCategory = 
                await dailyRecipesApiBroker.PostAsync(stringContent);
            var actualCategory = JsonConvert.DeserializeObject<CategoryResource>(await responseCategory.Content.ReadAsStringAsync());

            //then
            responseCategory.StatusCode.Should().Be(HttpStatusCode.OK);
            actualCategory.Should().BeEquivalentTo(expectedCategory);
            await dailyRecipesApiBroker.DeleteAsync(actualCategory.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task PostCategoriesAsync_TitleIsRequired()
        {
            // given
            var randomCategory = CreateRandomCategory();

            var inputCategory = randomCategory;
            inputCategory.Title = "";
            var json = JsonConvert.SerializeObject(inputCategory, Formatting.Indented);
            var stringContent = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            // when
            var responseCategory =
                await dailyRecipesApiBroker.PostAsync(stringContent);
            var actualCategory = JsonConvert.DeserializeObject<CategoryResource>(await responseCategory.Content.ReadAsStringAsync());

            //then
            responseCategory.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async System.Threading.Tasks.Task PostCategoriesAsync_ExcerptIsRequired()
        {
            // given
            var randomCategory = CreateRandomCategory();

            var inputCategory = randomCategory;
            inputCategory.Excerpt = "";
            var json = JsonConvert.SerializeObject(inputCategory, Formatting.Indented);
            var stringContent = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            // when
            var responseCategory =
                await dailyRecipesApiBroker.PostAsync(stringContent);
            var actualCategory = JsonConvert.DeserializeObject<CategoryResource>(await responseCategory.Content.ReadAsStringAsync());

            // then
            responseCategory.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCategoriesAsync_NoContent()
        {
            // when
            var responseCategory =
                await dailyRecipesApiBroker.GetAsync();
            var actualCategory = JsonConvert.DeserializeObject<List<CategoryResource>>(await responseCategory.Content.ReadAsStringAsync());

            // then
            responseCategory.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCategoriesAsync_Success()
        {
            // given
            var randomCategory = CreateRandomCategory();

            var inputCategory = randomCategory;
            var json = JsonConvert.SerializeObject(inputCategory, Formatting.Indented);
            var stringContent = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            var expectedCategory = inputCategory;

            // when
            var addedCategoryJson = await dailyRecipesApiBroker.PostAsync(stringContent);
            var addedCategory = JsonConvert.DeserializeObject<CategoryResource>(await addedCategoryJson.Content.ReadAsStringAsync());

            var responseCategory =
                await dailyRecipesApiBroker.GetAsync();
            var actualCategory = JsonConvert.DeserializeObject<List<CategoryResource>>(await responseCategory.Content.ReadAsStringAsync());

            // then
            responseCategory.StatusCode.Should().Be(HttpStatusCode.OK);
            responseCategory.Should().NotBeNull();
            await dailyRecipesApiBroker.DeleteAsync(addedCategory.Id);
        }
    }
}
