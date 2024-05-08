using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Ts3era.Dto.SubCategory_Dto;
using Ts3era.Repositories.Category_Repositories;
using Ts3era.Repositories.SubCategory_Repositories;

namespace Ts3era.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository repository;
        private readonly ICategoryRepository categoryRepository;

        public SubCategoryController(ISubCategoryRepository repository,ICategoryRepository categoryRepository )
        {
            this.repository = repository;
            this.categoryRepository = categoryRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryDetailsDto>>> GetAllSubCategory()
        {
            if(ModelState.IsValid)
            {

                var subcategory=await repository.GetAll();
                return Ok(subcategory);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubCategoryDetailsDto>>GetById(int id)
        {
            if(ModelState.IsValid)
            {

                var subcategory =await repository.GetById(id);
                if (subcategory == null)
                    return BadRequest("Not found SubCatgory");
                return Ok(subcategory);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<SubCategoryDetailsDto>>GetByName(string name)
        {
            if(ModelState.IsValid)
            {
                var subcategory =await repository.Getname(name);
                if (subcategory == null)
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = $"SubCategory{name} Not Found " });
                return Ok(subcategory);
            }
            return BadRequest(ModelState);

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryDetailsDto>>>SearchOfSubcategory(string ?name)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(name))
                    return await repository.GetAll();

                var subcategory =await repository.Search(name);
                if (!subcategory.Any(c => c.SubCategoryName == name))
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = $"not  found SubCategory with this name ( {name} )" });
                return Ok(subcategory);

            }

            return BadRequest(ModelState);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CreateSubCategoryDto>>CreateSubcategory([FromForm]CreateSubCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var subcategory=await repository.Create(dto);
                return Ok(subcategory);

            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<UpdateSubCategoryDto>>UpdateSubCategory(int id, [FromForm]UpdateSubCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var subcategory =await repository.Update(id, dto);
                return Ok(subcategory);

            }
            return BadRequest(ModelState);
        }

        [HttpGet]//using DropdownList
        public async Task<ActionResult<IEnumerable<CategoriesDto>>> GetAllCategory()
        {
            if (ModelState.IsValid)
            {
                var categories=await repository.GetAllCategory();
                return Ok(categories);

            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>Delete(int id)
        {
            if (ModelState.IsValid)
            {
                if (id != null)
                {
                    await repository.Delete(id);
                    return Ok("Deleted");
                }

            }
            return BadRequest($"Invalid ID{id}");
        }

    }
}
