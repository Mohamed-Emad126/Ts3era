using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ts3era.Dto.FeedBack_Dto;
using Ts3era.HandleResponseApi;
using Ts3era.Models;
using Ts3era.Models.Data;

namespace Ts3era.Repositories.FeedBack_Repository
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public FeedBackRepository(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<FeedBackDto> AddFeedback(FeedBackDto feedBackDto)
        {
            var feedmap = mapper.Map<FeedBack>(feedBackDto);
            await context.AddAsync(feedmap);
                  context.SaveChanges();
                  return feedBackDto;
        }

        public async Task<string> Delete(int feedbackid)
        {
            var message = string.Empty;

            var oldfeed =await context.feedBacks.FirstOrDefaultAsync(c=>c.Id==feedbackid);
            if ( oldfeed is null)
                message = "Not found FeedBack!";
            else
            {
                context.feedBacks.Remove(oldfeed);
                context.SaveChanges();
                message = "Deleted FeedBack";
            }
            return message;
        }


        public async Task<List<FeedBack>> GetAllFeedback()
        {
            var feed = await context.feedBacks.ToListAsync();
            return feed;
        }
    }
}
