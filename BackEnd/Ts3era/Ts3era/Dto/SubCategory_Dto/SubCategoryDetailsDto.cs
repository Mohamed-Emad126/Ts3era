namespace Ts3era.Dto.SubCategory_Dto
{
    public class SubCategoryDetailsDto
    {
        public string SubCategoryName { get; set; }
        public byte[] Image { get; set; }
        public string CategoryName { get; set; }
        public List<string> products { get; set; }

    }
}
