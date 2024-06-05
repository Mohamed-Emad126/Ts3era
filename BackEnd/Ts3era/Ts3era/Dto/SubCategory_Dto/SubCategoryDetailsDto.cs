﻿using Ts3era.Dto.ProductDto;

namespace Ts3era.Dto.SubCategory_Dto
{
    public class SubCategoryDetailsDto
    {
        public string SubCategoryName { get; set; }
        public string  Image { get; set; }
        public string CategoryName { get; set; }
        //public List<string> products { get; set; }
        public List<ProductDetailsDto> Products {  get; set; } 

    }
}
