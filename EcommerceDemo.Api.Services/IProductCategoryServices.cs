using EcommerceDemo.Api.ResourceModel;
using EcommerceDemo.Api.ResourceModel.Exceptions;
using EcommerceDemo.Core.DtoModel;
using EcommerceDemo.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceDemo.Api.Services
{
    public interface IProductCategoryServices
    {
        IEnumerable<ProductCategoryModel> GetAll(int limit, int offset, string sortBy, string sortDir, string searchTerm);
        ProductCategoryModel Get(int id);
        int Create(ProductCategoryModel productCategoryModel);
        int Update(int id, ProductCategoryModel productCategoryModel);
        bool Delete(int id);
    }

    public class ProductCategoryServices : IProductCategoryServices
    {
        private readonly IProductCategoryAppServices _productCategoryAppServices;

        public ProductCategoryServices(IProductCategoryAppServices productCategoryAppServices)
        {
            _productCategoryAppServices = productCategoryAppServices;
        }

        public int Create(ProductCategoryModel productCategoryModel)
        {
            ValidateRequestModel(productCategoryModel);

            try
            {
                return _productCategoryAppServices.Create(MapToDto(productCategoryModel));
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotCreateResource, ex.Message);
            }
        }

        private void ValidateRequestModel(ProductCategoryModel productAttributeModel)
        {
            if (productAttributeModel == null)
                throw new BadRequestException(ApiErrorCode.CanNotCreateResource, "model can't be empty");

            if (string.IsNullOrEmpty(productAttributeModel.CategoryName))
                throw new BadRequestException(ApiErrorCode.CanNotCreateResource, "category name can't be empty");
        }

        public bool Delete(int id)
        {
            if (id < 1)
                throw new BadRequestException(ApiErrorCode.CanNotDeleteResource, "invalid product category id");

            try
            {
                var result = _productCategoryAppServices.Get(id);
                if (result == null) 
                    throw new NotFoundException();
                return _productCategoryAppServices.Delete(id);
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotDeleteResource, ex.Message);
            }
        }

        public ProductCategoryModel Get(int id)
        {
            if (id < 1)
                throw new BadRequestException(ApiErrorCode.CanNotGetResource, "invalid product category id");

            try
            {
                var result = _productCategoryAppServices.Get(id);
                if (result == null)
                    throw new NotFoundException();
                return MapToResource(result);
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotGetResource, ex.Message);
            }
        }

        public IEnumerable<ProductCategoryModel> GetAll(int limit, int offset, string sortBy, string sortDir, string searchTerm)
        {
            try
            {
                var result = _productCategoryAppServices.GetAll(limit, offset, sortBy, sortDir, searchTerm);
                return result.Select(x => MapToResource(x));
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotGetResource, ex.Message);
            }
        }

        public int Update(int id, ProductCategoryModel productCategoryModel)
        {
            if (id < 1)
                throw new BadRequestException(ApiErrorCode.CanNotGetResource, "invalid product category id");

            ValidateRequestModel(productCategoryModel);

            try
            {
                var result = _productCategoryAppServices.Get(id);
                if (result == null)
                    throw new NotFoundException();

                return _productCategoryAppServices.Update(id, MapToDto(productCategoryModel));
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotUpdateResource, ex.Message);
            }
        }

        private ProductCategoryModel MapToResource(ProductCategoryDto result)
        {
            return new ProductCategoryModel()
            {
                CategoryId = result.CategoryId,
                CategoryName = result.CategoryName
            };
        }

        private ProductCategoryDto MapToDto(ProductCategoryModel productAttributeModel)
        {
            return new ProductCategoryDto()
            {
                CategoryId = productAttributeModel.CategoryId,
                CategoryName = productAttributeModel.CategoryName
            };
        }
    }
}
