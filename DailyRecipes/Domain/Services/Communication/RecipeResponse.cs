using DailyRecipes.Domain.Models;

namespace DailyRecipes.Domain.Services.Communication
{
    public class RecipeResponse : BaseResponse
    {
        public Recipe Recipe { get; private set; }

        private RecipeResponse(bool success, string message, Recipe recipe) : base(success, message)
        {
            Recipe = recipe;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="Recipe">Saved Recipe.</param>
        /// <returns>Response.</returns>
        public RecipeResponse(Recipe recipe) : this(true, string.Empty, recipe)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public RecipeResponse(string message) : this(false, message, null)
        { }
    }
}
