using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ts3era.Dto.FeedBack_Dto;
using Ts3era.Models;
using Ts3era.Repositories.FeedBack_Repository;

namespace Ts3era.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IFeedBackRepository repository;

        public ContactsController(IFeedBackRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<List<FeedBack>>> GetAllFeedBack()
        {
            var feed =await repository.GetAllFeedback();
            return Ok(feed);
        }
        [HttpPost]
        public async Task<ActionResult<FeedBackDto>>AddFeedBack(FeedBackDto dto)
        {
            if (ModelState.IsValid)
            {
                var feed = await repository.AddFeedback(dto);
                return Ok(feed);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<ActionResult>DeleteFeedBack(int Feedbackid)
        {
           if (ModelState.IsValid)
           {
                var feed = await repository.Delete(Feedbackid);
                if (feed != null)
                    return Ok(feed);
                else 
                    return BadRequest(feed );
           }
            return BadRequest(ModelState);

        }
    }
}
