using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyRecipes.Domain.Services;
using DailyRecipes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DailyRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<Program> _logger;
        public CategoriesController(ICategoryService categoryService, ILogger<Program> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IQueryable<Category>> GetCategories()
        {
            try
            {
                _logger.LogInformation("Fetching Categories List From Database");
                var result = _categoryService.GetCategories();
                if (result.Any())
                    return Ok(result);
                else
                    return NoContent();
            }
            catch(Exception e)
            {
                _logger.LogInformation(e, "Unable to get categories");
                return ValidationProblem(e.Message);
            }
            
        }
    }
}
