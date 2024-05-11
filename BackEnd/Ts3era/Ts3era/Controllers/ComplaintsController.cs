using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ts3era.Dto.ComplaintsDto;
using Ts3era.Dto.EmailsDto;
using Ts3era.Repositories.Complaint_Repositories;
using Ts3era.Services.EmailServices;

namespace Ts3era.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IComplaintRepository complaintRepository;
        private readonly IEmailServices emailServices;

        public ComplaintsController(
            IComplaintRepository complaintRepository,
            IEmailServices emailServices
            
            )
        {
            this.complaintRepository = complaintRepository;
            this.emailServices = emailServices;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplaintDto>>> GetAllComplaints()
        {
            if (ModelState.IsValid)
            {
                var complaint = await complaintRepository.GetAll();
                return Ok(complaint);

            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<ComplaintDto>>GetCmplaintById(int id)
        {
            if (ModelState.IsValid)
            {
                var compalint =await complaintRepository.GetById(id);
                if (compalint == null)
                    return StatusCode(404, new { Message = "Not Found Complaint" });
                return Ok(compalint);

            }
            return BadRequest(ModelState); 
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ComplaintDto>> AddComplaint([FromForm]ComplaintDto complaintDto)
        {
            if (ModelState.IsValid)
            {

                var complaint =await complaintRepository.Create(complaintDto);
                if (complaint != null)
                {
                    var email = new EmailRequestDto
                    {
                        EmailTO = complaint.Email,
                        Subject= "Complaint confirmation",
                        Body= $"Your complaint has been received and will be reviewed by officials and resolved immediately.\r\n\r\n.  Details of your complaint:{complaint.complaintDetails}"

                    };
                    await emailServices.SendEmail(email);
                    return StatusCode(200,
                        new { Message = $" Welcome {complaint.UserName}:The complaint has been received and will be sent to you via email" });

                }

            }
            return StatusCode(400, "An error occurred while sending the complaint. Please try again");

        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult>DeleteComplaint (int id)
        {
            if (ModelState.IsValid)
            {
                if (id!=null)
                { 
                    await  complaintRepository.Delete(id);
                    return StatusCode(401, new { Message = "Deleted Complaint " });

                }

            }
            return BadRequest(ModelState);
        }


        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult>GetCountComplaint()
        {
            if (ModelState.IsValid)
            {
                var count =await complaintRepository.GetCountAllComplaint();
                return Ok(count);

            }
            return BadRequest(ModelState);
        }
    }
}
