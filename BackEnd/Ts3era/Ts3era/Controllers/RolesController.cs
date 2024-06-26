﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ts3era.Dto.AuthanticationDto;
using Ts3era.Models;
using Ts3era.Services.Role_Services;

namespace Ts3era.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "admin")]

    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> signInManager;
        private readonly IRoleServices services;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public RolesController(
            RoleManager<IdentityRole>signInManager ,
            IRoleServices services,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IMapper mapper
            )
        {
            this.signInManager = signInManager;
            this.services = services;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult>AddNewRole(AddRoleDto dto)
        {
            if (ModelState.IsValid) 
            {

                var role = await services.create(dto);
                if (role == null)
                    return BadRequest();
               return Ok(role);

            }

           return  BadRequest(ModelState);
           
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
        {
            if (ModelState.IsValid)
            {
                var roles =await services.GetAll();
                return Ok(roles);

            }
            return BadRequest(ModelState);    
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<AddRoleDto>>UpdateROle (string id ,AddRoleDto dto)
        {
            if (ModelState.IsValid)
            {
                var role =await services.Update(id, dto);
                return Unauthorized(role);

            }
            return BadRequest(ModelState);     
        }
        [HttpDelete]
        public async Task <IActionResult>delete(string id)
        {
            if (ModelState.IsValid)
            {
                if (id != null)
                {
                    await services.Delete(id);
                    return Ok("Deleted Role");
                }
              
                //return StatusCode(401,new { Message =" Deleted Role " });
            }
            return BadRequest(ModelState);
        }


        [HttpGet("CheckBox")]//Test 
        public async Task<IActionResult>AdduserOrRemove (string roleid)
        {
            if (ModelState.IsValid)
            {
                var role =await roleManager.FindByIdAsync(roleid);
                if (role is null )
                    return BadRequest();

                var user =await userManager.Users.ToListAsync();
                var usersinrole =  new List<UserInRoleDto>();
                var map =mapper.Map<List<UserInRoleDto>>(user);


                foreach (var item in await userManager.Users.ToListAsync())
                {
                    var userinrole = new UserInRoleDto
                    {
                        UserId = item.Id,
                        UserName = item.UserName
                    };
                    if (await userManager.IsInRoleAsync(item,role.Name))
                        userinrole.Is_Selected = true;

                    else userinrole.Is_Selected= false;
                    map.Add(userinrole);

                    return Ok(map);
                }



            }
            return BadRequest(ModelState);
        }

        [HttpPost("CheckBox")]
        public async Task<IActionResult> AdduserOrRemove(List<UserInRoleDto> users,string roleid)
        {
            var role =await roleManager.FindByIdAsync (roleid);
            if (role is null )
                return BadRequest();
            if (ModelState.IsValid)
            {

                foreach (var user in users)
                {
                    var appuser = await userManager.FindByIdAsync(user.UserId);
                    
                    if(appuser != null)
                    {
                        // in check and no assign role 

                        if (user.Is_Selected && !(await userManager.IsInRoleAsync(appuser, role.Name)))
                            await userManager.AddToRoleAsync(appuser, role.Name);

                        //no check and assign role  

                        else if (!user.Is_Selected && (await userManager.IsInRoleAsync(appuser, role.Name)))
                            await userManager.RemoveFromRoleAsync(appuser, role.Name);

                    }

                    return Ok (users);


                }


            }
            return BadRequest(ModelState);   
        }

    }   
}
