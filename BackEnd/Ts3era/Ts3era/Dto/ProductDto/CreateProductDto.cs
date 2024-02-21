namespace Ts3era.Dto.ProductDto
{
    public class CreateProductDto
    {

        public string ProductName { get; set; }
        public IFormFile Image { get; set; }
        public float Price_From { get; set; }
        public float Price_TO { get; set; }
        public DateTime Last_Update { get; set; } = DateTime.Now;
        public string subCategoryName { get; set; }
    }
}
