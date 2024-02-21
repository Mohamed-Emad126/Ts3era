using System.ComponentModel.DataAnnotations.Schema;

namespace Ts3era.Models
{
    public class Product:BaseEntity
    {          
        public float Price_From  { get; set; }
        public float Price_TO   { get; set; }
        public DateTime Last_Update  {  get; set; }
        [ForeignKey(nameof(SubCategory))]
        public int SubCategory_Id { get; set; } 
        public SubCategory SubCategory { get; set; }

       

    }
}
