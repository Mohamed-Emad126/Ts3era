using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ts3era.Dto.ProductDto;
using Ts3era.Repositories.Product_Repositories;

namespace Ts3era.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetailsDto>>> GetAllProduct()
        {
            if (ModelState.IsValid)
            {
                var products =await repository.GetAll();
                return Ok(products);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<ProductDetailsDto>>GetById(int id)
        {
            if (ModelState.IsValid)
            {
                var product = await repository.GetById(id);
                if (product == null)
                    return StatusCode(StatusCodes.Status404NotFound, 
                        new { Message = $"Not Found Product With ID {id}" });
                return Ok(product); 
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<ProductDetailsDto>>GetByName(string name)
        {
            if (ModelState.IsValid)
            {
                var product = await repository.GetName(name);
                if (product == null)
                    return StatusCode(StatusCodes.Status404NotFound,
                        new { Message = $"Not Found Product With Name {name}" });
                return Ok(product);

            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetailsDto>>> SearchOfProduct(string? name)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(name))
                    return await repository.GetAll();

                var products = await repository.Search(name);
                if (!products.Any(c => c.ProductName == name))
                    return StatusCode(StatusCodes.Status404NotFound,
                        new { Message = $"not  found product with this name({name})" });
                return Ok(products);

            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoriesDto>>> GetAllSubCategories()
        {
            if (ModelState.IsValid)
            {
                var subCategories = await repository.GetAllSubCategories();
                return Ok(subCategories);

            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        public async Task<ActionResult<CreateProductDto>> CreateProduct([FromForm]CreateProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var product =await repository.Create(dto);
               
                return Ok(product);

            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateProductDto>>UpdateProduct(int id , [FromForm]UpdateProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var product=await repository.Update(id, dto);
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult>delete (int id)
        {
            if (ModelState.IsValid)
            {
                if (id !=null)
                {
                    await repository.Delete(id);
                    return Ok("Deleted Product ");
                }
            }
            return BadRequest("Invalid Product ");
        }
      
    }
}
