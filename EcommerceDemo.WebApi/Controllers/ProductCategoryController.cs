using EcommerceDemo.Api.ResourceModel;
using EcommerceDemo.Api.Services;
using System.Web.Http;

namespace EcommerceDemo.WebApi.Controllers
{
    [Route("product-category/{id}")]
    public class ProductCategoryController : CustomApiController
    {
        private readonly IProductCategoryServices _productCategoryService;

        public ProductCategoryController(IProductCategoryServices productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public IHttpActionResult Get(int id)
        => Ok(_productCategoryService.Get(id));

        public IHttpActionResult Put(int id, [FromBody] ProductCategoryModel model)
        => Ok(_productCategoryService.Update(id, model));

        public IHttpActionResult Delete(int id)
        => Ok(_productCategoryService.Delete(id));
    }
}