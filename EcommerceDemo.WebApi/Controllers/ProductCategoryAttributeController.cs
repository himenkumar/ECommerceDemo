using EcommerceDemo.Api.ResourceModel;
using EcommerceDemo.Api.Services;
using System.Web.Http;

namespace EcommerceDemo.WebApi.Controllers
{
    [Route("product-category-attribute/{id}")]
    public class ProductCategoryAttributeController : CustomApiController
    {
        private readonly IProductCategoryAttributeServices _productCategoryAttributeService;

        public ProductCategoryAttributeController
            (IProductCategoryAttributeServices productCategoryAttributeService)
        {
            _productCategoryAttributeService = productCategoryAttributeService;
        }

        public IHttpActionResult Get(int id)
            => Ok(_productCategoryAttributeService.Get(id));

        public IHttpActionResult Put(int id, [FromBody] ProductCategoryAttributeModel model)
            => Ok(_productCategoryAttributeService.Update(id, model));

        public IHttpActionResult Post([FromBody] ProductCategoryAttributeModel model)
            => Ok(_productCategoryAttributeService.Create(model));

        public IHttpActionResult Delete(int id)
            => Ok(_productCategoryAttributeService.Delete(id));
    }
}