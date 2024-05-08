namespace Ts3era.Dto.FavoriteProduct_Dtos
{
    public class DetailsFavProductDto
    {
        public string ProductName { get; set; }
        public float Price_From { get; set; }
        public float Price_To { get; set; }
        public DateTime LastUpdate { get; set; }
        public string SubCategoryName { get; set; }

        public byte[] Image { get; set; }
        public bool IsAvailable { get; set; }

    }
}
