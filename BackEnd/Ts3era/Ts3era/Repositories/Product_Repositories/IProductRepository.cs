using Ts3era.Dto.ProductDto;

namespace Ts3era.Repositories.Product_Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductDetailsDto>>GetAll();
        Task<ProductDetailsDto> GetById(int id);
        Task<ProductDetailsDto> GetName(string name);
        Task<List<ProductDetailsDto>> Search(string? name);
        Task<CreateProductDto> Create(CreateProductDto dto);

        Task<string>Update(int id ,UpdateProductDto dto);

        Task<List<SubCategoriesDto>> GetAllSubCategories();
        Task Delete(int id);
    }
}
