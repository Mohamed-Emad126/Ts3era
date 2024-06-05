using Ts3era.Dto.Category_Dto.Category_Dto;
using Ts3era.Dto.SubCategory_Dto;

namespace Ts3era.Repositories.SubCategory_Repositories
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategoryDetailsDto>> GetAll();
        Task<List<SubCategoryDetailsDto>> GetSubcatgeorywithCategoryId(int CategoryId);
        Task<SubCategoryDetailsDto> GetById(int id);
        Task<List<SubCategoryDetailsDto>> Search(string? name);
        Task<SubCategoryDetailsDto> Getname(string name);
        Task<CreateSubCategoryDto>Create(CreateSubCategoryDto dto);
        Task<string>Update (int id ,UpdateSubCategoryDto dto);
        Task<List<CategoriesDto>> GetAllCategory();
        Task Delete(int id);
        Task<bool>IsValidSubCategory(int id);
    }
}
