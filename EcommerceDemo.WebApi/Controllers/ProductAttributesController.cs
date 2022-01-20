using EcommerceDemo.Api.ResourceModel;
using EcommerceDemo.Api.Services;
using System.Web.Http;

namespace EcommerceDemo.WebApi.Controllers
{
    [Route("product-category/{productCategoryId}/attributes")]
    public class ProductAttributesController : CustomApiController
    {
        private readonly IProductCategoryAttributeServices _productCategoryAttributeService;

        public ProductAttributesController(IProductCategoryAttributeServices productCategoryAttributeService)
        {
            _productCategoryAttributeService = productCategoryAttributeService;
        }
        public IHttpActionResult Get(int productCategoryId)
        => Ok(_productCategoryAttributeService.GetByProductCategoryId(productCategoryId));

    }
}