using Microsoft.AspNetCore.Mvc;
using Reddis_NetCore6.Services;

namespace Reddis_NetCore6.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
           _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetCategories()
        {
            try
            {
                return Ok(_categoryService.GetAllCategory());

            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
