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

namespace DailyRecipes.Tests.Integration.APIs.Recipes
{
    [Collection(nameof(Brokers.ApiTestCollection))]
    public class RecipesApiTests
    {
        private readonly Brokers.DailyRecipesApiBroker dailyRecipesApiBroker;
        public RecipesApiTests(Brokers.DailyRecipesApiBroker dailyRecipesApiBroker) =>
            this.dailyRecipesApiBroker = dailyRecipesApiBroker;

        private SaveRecipeResource CreateRandomRecipe() =>
            new Filler<SaveRecipeResource>().Create();

        [Fact]
        public async System.Threading.Tasks.Task PostRecipesAsync_Success()
        {
            // given
            var randomRecipe = CreateRandomRecipe();

            var inputRecipe = randomRecipe;
            var json = JsonConvert.SerializeObject(inputRecipe, Formatting.Indented);
            var stringContent = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            var expectedRecipe = inputRecipe;

            // when
            var responseRecipe = 
                await dailyRecipesApiBroker.PostRecipeAsync(stringContent);
            var actualRecipe = JsonConvert.DeserializeObject<RecipeResource>(await responseRecipe.Content.ReadAsStringAsync());

            //then
            responseRecipe.StatusCode.Should().Be(HttpStatusCode.OK);
            actualRecipe.Should().NotBeNull();
            await dailyRecipesApiBroker.DeleteRecipeAsync(actualRecipe.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task PostRecipesAsync_TitleIsRequired()
        {
            // given
            var randomRecipe = CreateRandomRecipe();

            var inputRecipe = randomRecipe;
            inputRecipe.Title = "";
            var json = JsonConvert.SerializeObject(inputRecipe, Formatting.Indented);
            var stringContent = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            // when
            var responseRecipe =
                await dailyRecipesApiBroker.PostRecipeAsync(stringContent);
            var actualRecipe = JsonConvert.DeserializeObject<RecipeResource>(await responseRecipe.Content.ReadAsStringAsync());

            //then
            responseRecipe.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async System.Threading.Tasks.Task PostRecipesAsync_ExcerptIsRequired()
        {
            // given
            var randomRecipe = CreateRandomRecipe();

            var inputRecipe = randomRecipe;
            inputRecipe.Excerpt = "";
            var json = JsonConvert.SerializeObject(inputRecipe, Formatting.Indented);
            var stringContent = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            // when
            var responseRecipe =
                await dailyRecipesApiBroker.PostRecipeAsync(stringContent);
            var actualRecipe = JsonConvert.DeserializeObject<RecipeResource>(await responseRecipe.Content.ReadAsStringAsync());

            // then
            responseRecipe.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetRecipesAsync_NoContent()
        {
            // when
            var responseRecipe =
                await dailyRecipesApiBroker.GetRecipeAsync();
            var actualRecipe = JsonConvert.DeserializeObject<List<RecipeResource>>(await responseRecipe.Content.ReadAsStringAsync());

            // then
            responseRecipe.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
