using Ts3era.Dto.PortType_Dto;

namespace Ts3era.Repositories.Port_TypeRepositories
{
    public interface IPortType_Repositort
    {
        Task<List<PortType_Dto>> GetAllPortTypes();
    }
}
