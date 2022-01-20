using EcommerceDemo.Core.DtoModel;
using EcommerceDemo.DataAccess;
using EcommerceDemo.DataAccess.Domain;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace EcommerceDemo.Core.Services
{
    public interface IProductAppServices
    {
        IEnumerable<ProductDto> GetAll(int limit, int offset, string sortBy, string sortDir, string searchTerm);
        ProductDto Get(long id);
        long Create(ProductDto productDto);
        long Update(long id, ProductDto productDto);
        bool Delete(long id);
    }

    public class ProductAppServices : IProductAppServices
    {
        private readonly IUnitOfWork _uow;

        private IDictionary<string, string> sortByMapping = new Dictionary<string, string>()
        {
            { "product_id", "ProductId"},
            { "product_name", "ProdName"},
            { "product_description", "ProdDescription"}
        };

        private IDictionary<string, string> sortDirMapping = new Dictionary<string, string>()
        {
            { "Asc", "ascending"},
            { "Desc", "descending"}
        };

        public ProductAppServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<ProductDto> GetAll(int limit, int offset, string sortBy, string sortDir, string searchTerm)
        {   
            if (!sortByMapping.TryGetValue(sortBy, out string sortByField))
            {
                throw new InvalidServiceCallException("invalid sort by field");
            }

            if (!sortDirMapping.TryGetValue(sortDir, out string sortDirection))
            {
                throw new InvalidServiceCallException("invalid sort direction");
            }

            Expression<Func<Product, bool>> whereClause = BuildWhereClause(searchTerm);

            var products2 = _uow.ProductRepository.GetAll(whereClause, searchTerm,limit,offset, out int totalRecords, out int filteredRecords, sortDirection, sortByField).ToList();

            return products2 != null ? products2.Select(x=> MapToDto(x)): new List<ProductDto>();
        }

        private Expression<Func<Product, bool>> BuildWhereClause(string searchTerm)
        {   
            var predicate = PredicateBuilder.New<Product>(true); 

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split(' ').ToList().ConvertAll(x => x.ToLower());
                predicate = predicate.Or(s => searchTerms.Any(srch => s.ProdName.ToLower().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.ProdDescription.ToLower().Contains(srch)));
            }

            return predicate;
        }

        public ProductDto Get(long id)
        {
            var product = _uow.ProductRepository.GetByID(id);

            return product != null ? MapToDto(product) : null;
        }

        public long Create(ProductDto productDto)
        {
            if (productDto.Category == null)
            {
                throw new InvalidServiceCallException("Product category should be defined.");
            }

            using (var scope = new TransactionScope())
            {
                var product = new Product
                {
                    ProdName = productDto.ProductName,
                    ProdCatId = productDto.Category.CategoryId,
                    ProdDescription = productDto.ProductDescription,
                    ProductAttributes = productDto.Attributes?.Select(x => new ProductAttribute()
                    {
                        AttributeId = x.AttributeId,
                        AttributeValue = x.AttributeValue
                    }).ToList() ?? new List<ProductAttribute>()
                };

                _uow.ProductRepository.Insert(product);
                _uow.Save();
                scope.Complete();
                return (int)product.ProductId;
            }
        }

        public long Update(long id, ProductDto productDto)
        {
            if (productDto.Category == null)
            {
                throw new InvalidServiceCallException("Product category should be defined.");
            }

            using (var scope = new TransactionScope())
            {
                var product = _uow.ProductRepository.GetByID(id);
                product.ProdName = productDto.ProductName;
                product.ProdDescription = productDto.ProductDescription;
                product.ProdCatId = productDto.Category.CategoryId;

                var removedProductAttributeIds = product.ProductAttributes?.Where(x => !productDto.Attributes.Select(s => s.AttributeId).Contains(x.AttributeId))
                    .Select(x => x.ProductAttributeId).ToList() ?? new List<long>();

                foreach (var productAttributeId in removedProductAttributeIds)
                {
                    _uow.ProductAttributeRepository.Delete(productAttributeId);
                }

                var newProductAttributes = productDto.Attributes?.Where(x => !product.ProductAttributes.Select(s => s.AttributeId).Contains(x.AttributeId)).ToList();

                foreach (var newProductAttribute in newProductAttributes ?? Enumerable.Empty<ProductAttributeDto>())
                {
                    product.ProductAttributes.Add(new ProductAttribute()
                    {
                        AttributeId = newProductAttribute.AttributeId,
                        AttributeValue = newProductAttribute.AttributeValue
                    });
                }

                var existingProductAttributes = productDto.Attributes?.Where(x => product.ProductAttributes.Select(s => s.AttributeId).Contains(x.AttributeId)).ToList();

                foreach (var existingProductAttribute in existingProductAttributes ?? Enumerable.Empty<ProductAttributeDto>())
                {
                    product.ProductAttributes.First(x => x.AttributeId == existingProductAttribute.AttributeId).AttributeValue = existingProductAttribute.AttributeValue;
                }

                _uow.ProductRepository.Update(product);

                _uow.Save();
                scope.Complete();
                return product.ProductId;
            }
        }

        public bool Delete(long id)
        {
            using (var scope = new TransactionScope())
            {
                var product = _uow.ProductRepository.GetByID(id);

                if (product != null)
                {
                    var productAttributeIds = product.ProductAttributes?.Select(x => x.ProductAttributeId).ToList() ?? new List<long>();

                    foreach (var productAttributeId in productAttributeIds)
                    {
                        _uow.ProductAttributeRepository.Delete(productAttributeId);
                    }

                    _uow.ProductRepository.Delete(product);
                    _uow.Save();
                    scope.Complete();
                    return true;
                }
            }

            return false;
        }

        private ProductDto MapToDto(Product product)
        {
            return new ProductDto()
            {
                ProductId = product.ProductId,
                ProductName = product.ProdName,
                ProductDescription = product.ProdDescription,
                Category = new ProductCategoryDto()
                {
                    CategoryId = product.ProductCategory.ProdCatId,
                    CategoryName = product.ProductCategory.CategoryName
                },
                Attributes = product.ProductAttributes?.Select(x => new ProductAttributeDto()
                {
                    AttributeId = x.AttributeId,
                    AttributeValue = x.AttributeValue
                }) ?? new List<ProductAttributeDto>()
            };
        }
    }
}
