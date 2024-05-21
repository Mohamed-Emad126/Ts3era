using AutoMapper;
using AutoMapper.Execution;
using Ts3era.Dto.ProductDto;
using Ts3era.Models;

namespace Ts3era.ImageResolver
{
    public class ProductImageResolver:IValueResolver<Product ,ProductDetailsDto ,string >
    {
        private readonly IConfiguration confiq;

        public ProductImageResolver(IConfiguration confiq)
        {
            this.confiq = confiq;
        }
        public string Resolve(Product source, ProductDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))

                return
                confiq["baseurl"] + source.Image;
            return null;
        }
    }
}
