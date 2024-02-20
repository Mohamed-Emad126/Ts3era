using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ts3era.Models;
using Ts3era.Services.User_Services;

namespace Ts3era.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
        
    {
        private readonly IUserServices services;

        public UserController(IUserServices services)
        {
            this.services = services;
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult>Getall(string? search = "")
        {
            if (ModelState.IsValid)
            {
                var users = await services.GetAll(search);
                return Ok(users);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("int:id")]
        public async Task<IActionResult>GetbyId(string  id)
        {
            if (ModelState.IsValid)
            {
                var user = await services.GetById(id);
                return Ok (user);   
            }
            return BadRequest(ModelState);  
        }

        [HttpPut]
        public async Task<IActionResult>Update(string id,ApplicationUser user)
        {
            
            if (ModelState.IsValid)
            {
                var model =await services.Update(id, user);
                return Ok(model);

            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete (string id)
        {
            if (ModelState.IsValid)
            {
                var user =await services.Delete(id);
                return Unauthorized(user);

            }
            return BadRequest(ModelState);
        }
    }
}
