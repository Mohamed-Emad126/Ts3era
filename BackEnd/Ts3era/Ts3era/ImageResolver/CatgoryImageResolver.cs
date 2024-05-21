using AutoMapper;
using Ts3era.Dto.Category_Dto.Category_Dto;
using Ts3era.Dto.ProductDto;
using Ts3era.Models;

namespace Ts3era.ImageResolver
{
    public class CatgoryImageResolver : IValueResolver<Category, CategoryDetailsDto, string>
    {
        private readonly IConfiguration confiq;

        public CatgoryImageResolver(IConfiguration confiq )
        {
            this.confiq = confiq;
        }

        public string Resolve(Category source, CategoryDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))

                return
                confiq["baseurl"] + source.Image;
            return null;
        }
    }
}
