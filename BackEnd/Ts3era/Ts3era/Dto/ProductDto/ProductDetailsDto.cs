namespace Ts3era.Dto.ProductDto
{
    public class ProductDetailsDto
    {
        public string ProductName { get; set; }
        public float Price_From { get; set; }
        public float Price_To { get; set;}
        public DateTime LastUpdate { get; set; }
        public string SubCategoryName { get; set; } 

        public string  Image { get; set; }        
        public bool IsAvailable { get; set; }   

    }
}
