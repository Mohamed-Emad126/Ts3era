using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ts3era.Repositories.CategortRepositories;

namespace Ts3era.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategortRepository repository;

        public CategoryController(ICategortRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult>getall()
        {
            if (ModelState.IsValid)
            {
                var category = await repository.GetAll();
                return Ok(category);

            }
            return BadRequest(ModelState);  
        }

        [HttpGet("ByID")]
        public async Task <IActionResult>GetById(int id)
        {
            if (ModelState.IsValid)
            {
                var category=await repository.GetById(id);
                return Ok(category);

            }
            return BadRequest(ModelState);  
        }
    }
}
