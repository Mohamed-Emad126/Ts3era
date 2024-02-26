namespace Ts3era.Dto.Category_Dto.Category_Dto
{
    public class CategoryDetailsDto
    {
        public string CategoryName { get; set; }
        public byte[]Image { get; set; }
        public List<string> SubCategoriesNames { get; set; }
    }
}
