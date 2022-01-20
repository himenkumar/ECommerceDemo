using System.Collections.Generic;

namespace EcommerceDemo.Api.ResourceModel
{
    public class ProductModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public ProductCategoryModel Category { get; set; }
        public IEnumerable<ProductAttributeModel> Attributes { get; set; }
    }
}
