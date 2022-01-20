using EcommerceDemo.Api.ResourceModel;
using EcommerceDemo.Api.Services;
using System.Web.Http;

namespace EcommerceDemo.WebApi.Controllers
{
    [Route("product/{id}")]
    public class ProductController : CustomApiController
    {
        private readonly IProductServices _productService;

        public ProductController(IProductServices productServices)
        {
            _productService = productServices;
        }

        public IHttpActionResult Get(long id)
        => Ok(_productService.Get(id));

        public IHttpActionResult Put(long id, [FromBody] ProductModel model)
        => Ok(_productService.Update(id, model));
                
        public IHttpActionResult Delete(long id)
        => Ok(_productService.Delete(id));
    }
}