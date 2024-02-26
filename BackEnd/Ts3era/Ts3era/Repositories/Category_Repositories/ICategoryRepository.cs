using Microsoft.AspNetCore.Mvc.Routing;
using Ts3era.Dto.Category_Dto;
using Ts3era.Dto.Category_Dto.Category_Dto;
using Ts3era.HandleResponseApi;

namespace Ts3era.Repositories.Category_Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDetailsDto>> GetAll();
        Task<CategoryDetailsDto> GetById(int id);
        Task <CategoryDetailsDto> GetByName(string name);
        Task<CreateCategoryDto> Create(CreateCategoryDto dto);
        Task <string>Update (int id ,UpdateCategoryDto dto);
        List<CategoryDetailsDto> Search(string? name);
        Task Delete (int id);  
        Task<bool>isvaliidcategory(int id);

    }
}
