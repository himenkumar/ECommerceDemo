using EcommerceDemo.Api.ResourceModel;
using EcommerceDemo.Api.Services;
using System.Web.Http;

namespace EcommerceDemo.WebApi.Controllers
{
    [Route("product-category-attributes")]
    public class ProductCategoryAttributesController : CustomApiController
    {
        private readonly IProductCategoryAttributeServices _productCategoryAttributeService;

        public ProductCategoryAttributesController
            (IProductCategoryAttributeServices productCategoryAttributeService)
        {
            _productCategoryAttributeService = productCategoryAttributeService;
        }

        public IHttpActionResult Post([FromBody] ProductCategoryAttributeModel model)
        => Ok(_productCategoryAttributeService.Create(model));
    }
}