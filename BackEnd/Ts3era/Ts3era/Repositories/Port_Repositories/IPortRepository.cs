using Ts3era.Dto.GovernoratesDto;
using Ts3era.Dto.PortsDto;
using Ts3era.Dto.PortType_Dto;
using Ts3era.Dto.ProductDto;
using Ts3era.Models;
using Ts3era.Specifications;
using Ts3era.Specifications.PortSpecifications;

namespace Ts3era.Repositories.Port_Repositories
{
    public interface IPortRepository
    {
        Task<List<PortsDetailsDto>> GetAll();
        //Task<PortsDetailsDto> GetById(int id);
        Task<PortsDetailsDto> GetByName(string name);
        Task<List<PortsDetailsDto>> Search(string? name);
        Task<CreatePortDto>Create(CreatePortDto portDto);
        Task<string>Update(int id, PortDto portDto);
        Task Delete (int id);
      
        //Task<List<PortsDetailsDto>> getPortsWithGovernorateDtos(int? governorateId);

        //Task<List<PortsDetailsDto>> GetPortsWithPortType(int? id);

        Task<List<Ports>> GetAllPorts(ISpecification<Ports> specification);
        Task<Ports>GetPortById(ISpecification<Ports> specification);
        Task<List<PortsDetailsDto>> GetAllPortSpecs(PortSpecification specification);
        Task<PortsDetailsDto> GetPortspecs(int id );
        
    }
}
