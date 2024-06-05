namespace Ts3era.Dto.Category_Dto.Category_Dto
{
    public class CategoryDetailsDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string  Image { get; set; }
        public List<string> SubCategoriesNames { get; set; }
    }
}
