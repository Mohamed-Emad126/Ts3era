using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Ts3era.Dto.GovernoratesDto;
using Ts3era.Dto.PortsDto;
using Ts3era.Dto.PortType_Dto;
using Ts3era.Dto.ProductDto;
using Ts3era.Models;
using Ts3era.Models.Data;

namespace Ts3era.Repositories.Port_Repositories
{
    public class PortRepository:IPortRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public PortRepository(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

      

        public async Task<List<PortsDetailsDto>> GetAll()
        {
            var ports =await context.Ports
                .Include(c=>c.Governorates)
                .Include(c=>c.PortTypes)
                .ToListAsync();
            var map=mapper.Map<List<PortsDetailsDto>>(ports);
            return map;
        }

        public async  Task<PortsDetailsDto> GetById(int id)
        {
            var ports = await context.Ports
               .Include(c => c.Governorates)
               .Include(c => c.PortTypes)
               .FirstOrDefaultAsync(p=>p.Id==id);
            
            var map=mapper.Map<PortsDetailsDto>(ports);
            return map;
        }

        public async Task<PortsDetailsDto> GetByName(string name)
        {
            var ports = await context.Ports
              .Include(c => c.Governorates)
              .Include(c => c.PortTypes)
              .FirstOrDefaultAsync(p => p.Name == name);
            var map = mapper.Map<PortsDetailsDto>(ports);
            return map;
        }

        public async Task<List<PortsDetailsDto>> Search(string? name)
        {
            var search = await context.Ports
                 .Where(c => c.Name.Trim().ToLower()
                 .Contains(name.Trim().ToLower())).ToListAsync();
            var map = mapper.Map<List<PortsDetailsDto>>(search);
            return map;
        }

        public async Task<CreatePortDto> Create(CreatePortDto portDto)
        {
            var map =mapper.Map<Ports>(portDto);
             await context.Ports.AddAsync(map);
             await context.SaveChangesAsync();
            return portDto;
        }
        public async Task<string> Update(int id, PortDto portDto)
        {
           var port=await context.Ports.FirstOrDefaultAsync(c=>c.Id== id);
            if (port == null)
                throw new Exception("Not Found Ports ");
            port.Name = portDto.PortName;
            port.Governorate_Id = portDto.Governorate;
            port.PortType_Id = portDto.Types;
            context.Update(port);
            context.SaveChanges();
            return "Updated Ports";
        }

        public async Task Delete(int id)
        {

            var port=await context.Ports.FirstOrDefaultAsync(p=>p.Id== id);
            if (port == null || id != port.Id)
            {
                throw new Exception($"Invalid Port With Id {id}");
            } 
            else
            {
                context.Remove(port);
                await context.SaveChangesAsync();
            }

        }

        public async Task<List<PortsDetailsDto>> getPortsWithGovernorateDtos(int? governorateId)
        {
            var port=await context.Ports
               .Where(c=>c.Governorate_Id==governorateId)
                .Include(c=>c.Governorates)
                .Include(c=>c.PortTypes)
                .ToListAsync();
            var map=mapper.Map<List<PortsDetailsDto>>(port);
            return map;
        }
        public async Task<List<PortsDetailsDto>> GetPortsWithPortType(int? id)
        {
            var port=await context.Ports
                .Where(c=>c.PortType_Id==id)
                .Include(c=>c.PortTypes)
                .Include(c=>c.Governorates)
                .ToListAsync();
          
            var map = mapper.Map<List<PortsDetailsDto>>(port);
            return map;
        

        }


    }
}
