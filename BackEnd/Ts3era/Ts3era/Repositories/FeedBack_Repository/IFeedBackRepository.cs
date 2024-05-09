using Ts3era.Dto.FeedBack_Dto;
using Ts3era.Models;

namespace Ts3era.Repositories.FeedBack_Repository
{
    public interface IFeedBackRepository
    {

        Task<FeedBackDto> AddFeedback(FeedBackDto feedBackDto);
        Task<List<FeedBack>> GetAllFeedback();
        Task<string> Delete(int feedbackid);
    }
}
