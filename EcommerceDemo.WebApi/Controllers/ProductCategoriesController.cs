using EcommerceDemo.Api.ResourceModel;
using EcommerceDemo.Api.Services;
using System.Web.Http;

namespace EcommerceDemo.WebApi.Controllers
{
    [Route("product-categories")]
    public class ProductCategoriesController : CustomApiController
    {
        private readonly IProductCategoryServices _productCategoryServices;

        public ProductCategoriesController(IProductCategoryServices productCategoryServices)
        {
            _productCategoryServices = productCategoryServices;
        }

        public IHttpActionResult Post([FromBody] ProductCategoryModel model)
        => Ok(_productCategoryServices.Create(model));

        public IHttpActionResult Get(int limit = 100, int offset = 0, string sortBy = "CategoryName", string sortDir = "Asc", string searchTerm = "")
        => Ok(_productCategoryServices.GetAll(limit, offset, sortBy, sortDir, searchTerm));
    }
}