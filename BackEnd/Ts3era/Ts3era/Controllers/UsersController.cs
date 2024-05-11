using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ts3era.Dto.UsersDto;
using Ts3era.Services.User_Services;

namespace Ts3era.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices userServices;


        public UsersController(IUserServices userServices)
        {
            this.userServices = userServices;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailsDto>>> GetUsers(string? Search)
        {
            var users =await userServices.GetAllUsers(Search);
            return Ok(users);
        }
        [HttpGet]
        public async Task<ActionResult<UserDetailsDto>>GetUserById(string UserId)
        {
            var user=await userServices.GetDeatailsById(UserId);
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<userdto>>> GetUsersUsingDropDown()
        {
            var users =await userServices.GetAllUserDropDown();
            return Ok(users);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string UserId)
        {
            if (ModelState.IsValid)
            {
                var user = await userServices.Delete(UserId);
                return Ok(user);
            }
            return BadRequest(ModelState);
        }


    }
}
