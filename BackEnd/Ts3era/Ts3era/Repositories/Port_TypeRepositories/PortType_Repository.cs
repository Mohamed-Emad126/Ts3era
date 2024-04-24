using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ts3era.Dto.PortType_Dto;
using Ts3era.Models.Data;

namespace Ts3era.Repositories.Port_TypeRepositories
{
    public class PortType_Repository : IPortType_Repositort
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PortType_Repository(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<PortType_Dto>> GetAllPortTypes()
        { 
            var type =await context.PortTypes.ToListAsync();
            
            var map =mapper.Map<List<PortType_Dto>>(type);
            return map;
        }
    }
}
