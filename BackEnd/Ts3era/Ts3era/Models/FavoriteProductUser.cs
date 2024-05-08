using System.ComponentModel.DataAnnotations.Schema;

namespace Ts3era.Models
{
    public class FavoriteProductUser
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(product))]
        public int productId { get; set; }  
        public Product product { get; set; }
    }
}
