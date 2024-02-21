using System.ComponentModel.DataAnnotations.Schema;

namespace Ts3era.Models
{
    public class SubCategory:BaseEntity
    {
        [ForeignKey(nameof(Category))]
        public int Category_Id { get; set; }
        public Category Category { get; set; }
        public List<Product> Products { get; set; } 

    }
}
