using System.Collections.Generic;

namespace EcommerceDemo.Core.DtoModel
{
    public class ProductDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public ProductCategoryDto Category { get; set; }
        public IEnumerable<ProductAttributeDto> Attributes { get; set; }
    }

}
