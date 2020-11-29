using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DailyRecipes.Domain.Services;
using DailyRecipes.Extensions;
using DailyRecipes.Models;
using DailyRecipes.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DailyRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RecipesController : ControllerBase
    {
            private readonly IRecipeService _recipeService;
            private readonly ILogger<Program> _logger;
            private readonly IMapper _mapper;

            public RecipesController(IRecipeService RecipeService, ILogger<Program> logger, IMapper mapper)
            {
                _recipeService = RecipeService;
                _logger = logger;
                _mapper = mapper;
            }

            [HttpGet]
            [Route("")]
            [Produces("application/json")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            public IActionResult GetRecipes()
            {
                try
                {
                    _logger.LogInformation("Fetching Recipes List From Database");
                    var result = _recipeService.GetRecipes();
                    var Recipes_resources = _mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeResource>>(result);
                    if (result.Any())
                        return Ok(Recipes_resources);
                    else
                        return NoContent();
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e, "Unable to get Recipes");
                    return ValidationProblem(e.Message);
                }

            }

            [HttpPost]
            [Route("")]
            [Produces("application/json")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public IActionResult SaveRecipe([FromBody] SaveRecipeResource resource)
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState.GetErrorMessages());

                    var Recipe = _mapper.Map<SaveRecipeResource, Recipe>(resource);
                    var result = _recipeService.SaveRecipe(Recipe);

                    if (!result.Success)
                        return BadRequest(result.Success);

                    var RecipeResource = _mapper.Map<Recipe, RecipeResource>(result.Recipe);
                    return Ok(RecipeResource);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e, "An error occurred when saving the Recipe");
                    return ValidationProblem(e.Message);
                }
            }

            [HttpPut("{id}")]
            [Produces("application/json")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public IActionResult UpdateRecipe(Guid id, [FromBody] SaveRecipeResource resource)
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState.GetErrorMessages());

                    var Recipe = _mapper.Map<SaveRecipeResource, Recipe>(resource);
                    var result = _recipeService.UpdateRecipe(id, Recipe);

                    if (!result.Success)
                        return BadRequest(result.Success);

                    var RecipeResource = _mapper.Map<Recipe, RecipeResource>(result.Recipe);
                    // Updating Id in case of update
                    RecipeResource.Id = id;
                    return Ok(RecipeResource);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e, "An error occurred when saving the Recipe");
                    return ValidationProblem(e.Message);
                }
            }

            [HttpDelete("{id}")]
            [Produces("application/json")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public IActionResult DeleteRecipe(Guid id)
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState.GetErrorMessages());

                    var result = _recipeService.DeleteRecipe(id);

                    if (!result.Success)
                        return BadRequest(result.Success);

                    var RecipeResource = _mapper.Map<Recipe, RecipeResource>(result.Recipe);
                    return Ok(RecipeResource);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e, "An error occurred when saving the Recipe");
                    return ValidationProblem(e.Message);
                }
        }
    }
}
