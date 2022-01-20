using EcommerceDemo.Core.DtoModel;
using EcommerceDemo.DataAccess;
using EcommerceDemo.DataAccess.Domain;
using LinqKit;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace EcommerceDemo.Core.Services
{
    public interface IProductCategoryAttributeAppServices
    {
        ProductCategoryAttributeDto Get(int id);
        IEnumerable<ProductCategoryAttributeDto> GetByProductCategoryId(int productCategoryId);  
        int Create(ProductCategoryAttributeDto productCategoryAttributeDto);
        int Update(int id, ProductCategoryAttributeDto productCategoryAttributeDto);
        bool Delete(int id);
    }

    public class ProductCategoryAttributeAppServices : IProductCategoryAttributeAppServices
    {
        private readonly IUnitOfWork _uow;
        public ProductCategoryAttributeAppServices(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public int Create(ProductCategoryAttributeDto productCategoryAttributeDto)
        {
            using (var scope = new TransactionScope())
            {
                var productCategoryAttribute = new ProductAttributeLookup
                {
                    ProdCatId = productCategoryAttributeDto.ProductCategory.CategoryId,
                    AttributeName = productCategoryAttributeDto.AttributeName
                };

                _uow.ProductCategoryAttributeRepository.Insert(productCategoryAttribute);
                _uow.Save();
                scope.Complete();
                return productCategoryAttribute.AttributeId;
            }
        }

        public bool Delete(int id)
        {
            using (var scope = new TransactionScope())
            {
                var productCategoryAttribute = _uow.ProductCategoryAttributeRepository.GetByID(id);

                if (productCategoryAttribute != null)
                {
                    _uow.ProductCategoryAttributeRepository.Delete(productCategoryAttribute);
                    _uow.Save();
                    scope.Complete();
                    return true;
                }
            }

            return false;
        }

        public ProductCategoryAttributeDto Get(int id)
        {
            var productCategoryAttribute = _uow.ProductCategoryAttributeRepository.GetByID(id);

            if (productCategoryAttribute != null)
            {
                return MapToDto(productCategoryAttribute);
            }

            return null;
        }

        public IEnumerable<ProductCategoryAttributeDto> GetByProductCategoryId(int productCategoryId)
        {
            var predicate = PredicateBuilder.New<ProductAttributeLookup>(true).Or(s => productCategoryId == s.ProdCatId);

            var productCategoryAttribute = _uow.ProductCategoryAttributeRepository.GetManyQueryable(predicate).ToList();

            return productCategoryAttribute.Select(x => MapToDto(x));
        }

        public int Update(int id, ProductCategoryAttributeDto productCategoryAttributeDto)
        {
            using (var scope = new TransactionScope())
            {
                var productCategoryAttribute = _uow.ProductCategoryAttributeRepository.GetByID(id);

                productCategoryAttribute.AttributeName = productCategoryAttributeDto.AttributeName;
                productCategoryAttribute.ProdCatId = productCategoryAttributeDto.ProductCategory.CategoryId;

                _uow.ProductCategoryAttributeRepository.Update(productCategoryAttribute);

                _uow.Save();
                scope.Complete();
                return productCategoryAttribute.AttributeId;
            }

        }

        private ProductCategoryAttributeDto MapToDto(ProductAttributeLookup productCategoryAttribute)
        {
            return new ProductCategoryAttributeDto()
            {
                AttributeId = productCategoryAttribute.AttributeId,
                AttributeName = productCategoryAttribute.AttributeName,
                ProductCategory = new ProductCategoryDto()
                {
                    CategoryId = productCategoryAttribute.ProductCategory.ProdCatId,
                    CategoryName = productCategoryAttribute.ProductCategory.CategoryName
                }
            };
        }
    }
}
