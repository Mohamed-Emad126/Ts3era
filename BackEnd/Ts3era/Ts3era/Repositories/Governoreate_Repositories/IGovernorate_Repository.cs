using Ts3era.Dto.GovernoratesDto;
using Ts3era.Dto.PortsDto;

namespace Ts3era.Repositories.Governoreate_Repositories
{
    public interface IGovernorate_Repository
    {
        Task<List<governorateDto>> GetAllGovernorate();
    }
}
