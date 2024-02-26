using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ts3era.Models;
using Ts3era.Services.User_Services;

namespace Ts3era.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
        
    {
        private readonly IUserServices services;

        public UserController(IUserServices services)
        {
            this.services = services;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllUsers(string? search = "")
        {
            if (ModelState.IsValid)
            {
                var users = await services.GetAll(search);
                return Ok(users);
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        public async Task<IActionResult>GetbyId(string  id)
        {
            if (ModelState.IsValid)
            {
                var user = await services.GetById(id);
                return Ok (user);   
            }
            return BadRequest(ModelState);  
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApplicationUser>>UpdateUser(string id,ApplicationUser user)
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
