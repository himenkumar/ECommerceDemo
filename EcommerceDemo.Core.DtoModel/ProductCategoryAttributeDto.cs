namespace EcommerceDemo.Core.DtoModel
{
    public class ProductCategoryAttributeDto
    {
        public int AttributeId { get; set; }
        public ProductCategoryDto ProductCategory { get; set; }
        public string AttributeName { get; set; }
    }
}
