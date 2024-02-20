using Ts3era.Dto.EmailsDto;

namespace Ts3era.Services.EmailServices
{
    public interface IEmailServices
    {
        Task SendEmail(EmailRequestDto mail);
    }
}
