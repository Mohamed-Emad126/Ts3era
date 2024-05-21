using AutoMapper;
using Ts3era.Dto.SubCategory_Dto;
using Ts3era.Models;

namespace Ts3era.ImageResolver
{
    public class SubCategoryRevolver : IValueResolver<SubCategory, SubCategoryDetailsDto, string>
    {
        private readonly IConfiguration confiq;

        public SubCategoryRevolver(IConfiguration confiq )
        {
            this.confiq = confiq;
        }
        public string Resolve(SubCategory source, SubCategoryDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))

                return
                confiq["baseurl"] + source.Image;
            return null;
        }
    }
}
