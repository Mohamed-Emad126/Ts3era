using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Ts3era.Dto.ProductDto
{
    public class BaseProductDto
    {
        public string ProductName { get; set; }
        public float Price_From { get; set; }
        public float Price_To { get; set; }
        public DateTime Last_Update { get; set; } =DateTime.Now;
        public int SubCategory_ID { get; set; } 

    }
}
