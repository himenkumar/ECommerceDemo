using EcommerceDemo.Api.ResourceModel;
using EcommerceDemo.Api.Services;
using System.Web.Http;

namespace EcommerceDemo.WebApi.Controllers
{
    [Route("products")]
    public class ProductsController : CustomApiController
    {
        private readonly IProductServices _productService;

        public ProductsController(IProductServices productServices)
        {
            _productService = productServices;
        }
        public IHttpActionResult Post([FromBody] ProductModel model)
        => Ok(_productService.Create(model));        

        public IHttpActionResult Get(int limit = 100,int offset = 0, string sortBy = "product_id", string sortDir = "Desc", string searchTerm = "")
        => Ok(_productService.GetAll(limit, offset, sortBy, sortDir, searchTerm));
    }
}