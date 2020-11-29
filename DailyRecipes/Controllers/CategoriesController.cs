using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyRecipes.Domain.Services;
using DailyRecipes.Models;
using DailyRecipes.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DailyRecipes.Extensions;
using AutoMapper;

namespace DailyRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<Program> _logger;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, ILogger<Program> logger, IMapper mapper)
        {
            _categoryService = categoryService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetCategories()
        {
            try
            {
                _logger.LogInformation("Fetching Categories List From Database");
                var result = _categoryService.GetCategories();
                var categories_resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(result);
                if (result.Any())
                    return Ok(categories_resources);
                else
                    return NoContent();
            }
            catch(Exception e)
            {
                _logger.LogInformation(e, "Unable to get categories");
                return ValidationProblem(e.Message);
            }
            
        }

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SaveCategory([FromBody] SaveCategoryResource resource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessages());

                var category = _mapper.Map<SaveCategoryResource, Category>(resource);
                var result = _categoryService.SaveCategory(category);

                if (!result.Success)
                    return BadRequest(result.Success);

                var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
                return Ok(categoryResource);
            }
            catch(Exception e)
            {
                _logger.LogInformation(e, "An error occurred when saving the category");
                return ValidationProblem(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCategory(Guid id, [FromBody] SaveCategoryResource resource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessages());

                var category = _mapper.Map<SaveCategoryResource, Category>(resource);
                var result = _categoryService.UpdateCategory(id, category);

                if (!result.Success)
                    return BadRequest(result.Success);

                var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
                // Updating Id in case of update
                categoryResource.Id = id;
                return Ok(categoryResource);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "An error occurred when saving the category");
                return ValidationProblem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCategory(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessages());

                var result = _categoryService.DeleteCategory(id);

                if (!result.Success)
                    return BadRequest(result.Success);

                var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
                return Ok(categoryResource);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "An error occurred when saving the category");
                return ValidationProblem(e.Message);
            }
        }
    }
}
