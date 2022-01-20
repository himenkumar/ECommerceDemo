namespace EcommerceDemo.Api.ResourceModel
{
    public class ProductCategoryAttributeModel
    {
        public int AttributeId { get; set; }
        public ProductCategoryModel ProductCategory { get; set; }
        public string AttributeName { get; set; }
    }
}
