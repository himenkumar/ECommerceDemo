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
    public interface IProductCategoryAppServices
    {
        IEnumerable<ProductCategoryDto> GetAll(int limit, int offset, string sortBy, string sortDir, string searchTerm);
        ProductCategoryDto Get(int id);
        int Create(ProductCategoryDto productCategoryDto);
        int Update(int id, ProductCategoryDto productCategoryDto);
        bool Delete(int id);
    }

    public class ProductCategoryAppServices : IProductCategoryAppServices
    {
        private readonly IUnitOfWork _uow;
        public ProductCategoryAppServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public int Create(ProductCategoryDto productCategoryDto)
        {
            using (var scope = new TransactionScope())
            {
                var productCategory = new ProductCategory
                {
                    ProdCatId = productCategoryDto.CategoryId,
                    CategoryName = productCategoryDto.CategoryName
                };

                _uow.ProductCategoryRepository.Insert(productCategory);
                _uow.Save();
                scope.Complete();
                return productCategory.ProdCatId;
            }
        }

        public bool Delete(int id)
        {
            using (var scope = new TransactionScope())
            {
                var productCategory = _uow.ProductCategoryRepository.GetByID(id);

                if (productCategory != null)
                {
                    _uow.ProductCategoryRepository.Delete(productCategory);
                    _uow.Save();
                    scope.Complete();
                    return true;
                }
            }

            return false;
        }

        public ProductCategoryDto Get(int id)
        {
            var productCategory = _uow.ProductCategoryRepository.GetByID(id);

            if (productCategory != null)
            {
                return MapToDto(productCategory);
            }

            return null;
        }

        public IEnumerable<ProductCategoryDto> GetAll(int limit, int offset, string sortBy, string sortDir, string searchTerm)
        {
            Expression<Func<ProductCategory, bool>> whereClause = BuildWhereClause(searchTerm);

            var sortByField = "CategoryName";
            var sortDirection = "ascending";

            var productsCategoryies = _uow.ProductCategoryRepository.GetAll(whereClause, searchTerm, limit, offset, out int totalRecords, out int filteredRecords, sortDirection, sortByField).ToList();

            return productsCategoryies != null ? productsCategoryies.Select(x => MapToDto(x)) : new List<ProductCategoryDto>();
        }        

        public int Update(int id, ProductCategoryDto productCategoryDto)
        {
            using (var scope = new TransactionScope())
            {
                var productCategory = _uow.ProductCategoryRepository.GetByID(id);
                
                productCategory.CategoryName = productCategoryDto.CategoryName;

                _uow.ProductCategoryRepository.Update(productCategory);

                _uow.Save();
                scope.Complete();
                return productCategory.ProdCatId;
            }
        }

        private Expression<Func<ProductCategory, bool>> BuildWhereClause(string searchTerm)
        {
            var predicate = PredicateBuilder.New<ProductCategory>(true);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split(' ').ToList().ConvertAll(x => x.ToLower());
                predicate = predicate.Or(s => searchTerms.Any(srch => s.CategoryName.ToLower().Contains(srch)));
            }

            return predicate;
        }

        private ProductCategoryDto MapToDto(ProductCategory productCategory)
        {
            return new ProductCategoryDto()
            {
                CategoryId = productCategory.ProdCatId,
                CategoryName = productCategory.CategoryName
            };
        }
    }
}
