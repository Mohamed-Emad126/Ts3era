namespace Ts3era.Models
{
    public class Category:BaseEntity
    {
        public List<SubCategory> subCategories { get; set; } = new List<SubCategory>();
    }
}
