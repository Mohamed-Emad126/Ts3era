using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using Ts3era.Dto.Category_Dto;
using Ts3era.Dto.Category_Dto.Category_Dto;
using Ts3era.HandleResponseApi;
using Ts3era.Models.Data;
using Ts3era.Repositories.Category_Repositories;

namespace Ts3era.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repo;
        private readonly ApplicationDbContext context;

        public CategoryController(ICategoryRepository repo,ApplicationDbContext context)
        {
            this.repo = repo;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDetailsDto>>>GetAll()
        {
            if (ModelState.IsValid)
            {
                var category = await repo.GetAll();
                return Ok(category);

            }
            return BadRequest(ModelState);    
        } 
        
        [HttpGet]
        public async Task<ActionResult<CategoryDetailsDto>>GetById(int id )
        {
            if (ModelState.IsValid)
            {
                var category = await repo.GetById(id);
                if (category == null)
                    return BadRequest("Not founf Category");
                return Ok(category);

            }
            return BadRequest(ModelState);    
        }


      
        

        [HttpGet]
        public async Task<ActionResult<CategoryDetailsDto>>GetByName (string name)
        {
            if (ModelState.IsValid)
            {
                var category =await repo.GetByName(name);
                return Ok(category);

            }
            return BadRequest(ModelState);  
        }

        [HttpGet]
        public async Task<ActionResult> GetCountCategory()
        {
            if (ModelState.IsValid)
            {
                var category = await repo.GetCountCategory();
                return Ok(category);

            }
            return BadRequest(ModelState);  
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CreateCategoryDto>>CreateCategory([FromForm]CreateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var category= await repo.Create(dto);
                return Ok(category);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UpdateCategoryDto>>UpdateCategory(int id ,[FromForm]UpdateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {

                var category=await repo.Update(id, dto);
                return Ok(category);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDetailsDto>>>SearchOfCategory(string? name)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(name))
                {
                    var category = await repo.GetAll();
                    return Ok(category);
                }
                else
                {
                    var category=repo.Search(name);
                    if (!category.Any(c => c.CategoryName == name))//category not found in search 
                        return BadRequest("Not found ");
                    return Ok(category);

                }

            }
            return BadRequest(ModelState);
        }

        

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>Delete (int id)
        {
            if (ModelState.IsValid)
            {
                if (id != null)
                {
                    await repo.Delete(id);
                    return Ok("removed");
                }
            }
            return BadRequest($"Inalid ID{id}");
        }
      
    }
}
