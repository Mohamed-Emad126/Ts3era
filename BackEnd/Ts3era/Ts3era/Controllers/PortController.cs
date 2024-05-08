using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ts3era.Dto.GovernoratesDto;
using Ts3era.Dto.PortsDto;
using Ts3era.Dto.PortType_Dto;
using Ts3era.Repositories.Governoreate_Repositories;
using Ts3era.Repositories.Port_Repositories;
using Ts3era.Repositories.Port_TypeRepositories;

namespace Ts3era.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PortController : ControllerBase
    {
        private readonly IPortRepository portRepository;
        private readonly IGovernorate_Repository governorate_Repository;
        private readonly IPortType_Repositort portType_Repositort;

        public PortController(IPortRepository portRepository, IGovernorate_Repository governorate_Repository, IPortType_Repositort portType_Repositort)
        {
            this.portRepository = portRepository;
            this.governorate_Repository = governorate_Repository;
            this.portType_Repositort = portType_Repositort;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortsDetailsDto>>> GetAllPorts()
        {
            if (ModelState.IsValid)
            {
                var ports = await portRepository.GetAll();
                return Ok(ports);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<PortsDetailsDto>> GetPortById(int id)
        {
            if (ModelState.IsValid)
            {
                var port = await portRepository.GetById(id);
                if (port == null)
                    return StatusCode(StatusCodes.Status404NotFound,
                        new { Message = "Invalid Port" });
                return Ok(port);

            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<PortsDetailsDto>> GetportByName(string name)
        {
            if (ModelState.IsValid)
            {
                var port = await portRepository.GetByName(name);
                if (port == null)
                    return StatusCode(StatusCodes.Status404NotFound,
                        new { Message = $"Not Found port With Name {name}" });
                return Ok(port);


            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortsDetailsDto>>> SearchOfPorts(string? name)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(name))
                {
                    return await portRepository.GetAll();
                }
                else
                {
                    var port = await portRepository.Search(name);
                    if (!port.Any(c => c.PortName == name))
                        return StatusCode(StatusCodes.Status404NotFound,
                            new { Message = $"Not Found Port With Name{name}" });
                    return Ok(port);
                }

            }
            return BadRequest(ModelState);
        }

      

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortsDetailsDto>>> GetPortWithPortType(int? TypeId)
        {
            if (ModelState.IsValid)
            {
                if (TypeId == null)
                    return await portRepository.GetAll();

                var porttype = await portRepository.GetPortsWithPortType(TypeId);
                return Ok(porttype);
            }
            return BadRequest(ModelState);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortsDetailsDto>>> GetPortWithGovernorate(int? governorateId)
        {

            if (ModelState.IsValid)
            {
                if (governorateId == null)
                    return await portRepository.GetAll();

                var port = await portRepository.getPortsWithGovernorateDtos(governorateId);
                return Ok(port);


            }
            return BadRequest(ModelState);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortType_Dto>>> GetAllPortTypes()
        {
            if (ModelState.IsValid)
            {
                var portTypes = await portType_Repositort.GetAllPortTypes();
                return Ok(portTypes);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<governorateDto>>> GetAllGovernorates()
        {
            if (ModelState.IsValid)
            {

                var governorate=await governorate_Repository.GetAllGovernorate();
                return Ok(governorate);
            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        public async Task<ActionResult<CreatePortDto>> Createport([FromForm] CreatePortDto dto)
        {
            if (ModelState.IsValid)
            {
                var port = await portRepository.Create(dto);
                return Ok(port);

            }
            return BadRequest(ModelState);

        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<PortDto>>UpdatePort(int id , PortDto dto)
        {
            if (ModelState.IsValid)
            {
                var port = await portRepository.Update(id, dto);
                return Ok(port);

            }
            return BadRequest(ModelState);

        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult>Delete(int id)
        {
            if (ModelState.IsValid)
            {
                if (id !=null)
                {
                     await portRepository.Delete(id);
                     return StatusCode(StatusCodes.Status401Unauthorized,
                        new { Message = $"Deleted Port " });
                }

            }
            return BadRequest("Invalid Port");
        }


       
    }
}
