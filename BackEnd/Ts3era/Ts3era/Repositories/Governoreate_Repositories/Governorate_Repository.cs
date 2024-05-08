using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ts3era.Dto.GovernoratesDto;
using Ts3era.Models.Data;

namespace Ts3era.Repositories.Governoreate_Repositories
{
    public class Governorate_Repository : IGovernorate_Repository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public Governorate_Repository(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<governorateDto>> GetAllGovernorate()
        {
            var governorate=await context.Governorates.OrderBy(c=>c.Name).ToListAsync  ();

            var map = mapper.Map<List<governorateDto>>(governorate);
            return map;
        }
    }
}
