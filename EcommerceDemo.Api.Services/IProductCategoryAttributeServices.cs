using EcommerceDemo.Api.ResourceModel;
using EcommerceDemo.Api.ResourceModel.Exceptions;
using EcommerceDemo.Core.DtoModel;
using EcommerceDemo.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceDemo.Api.Services
{
    public interface IProductCategoryAttributeServices
    {
        ProductCategoryAttributeModel Get(int id);
        IEnumerable<ProductCategoryAttributeModel> GetByProductCategoryId(int productCategoryId);
        int Create(ProductCategoryAttributeModel productAttributeModel);
        int Update(int id, ProductCategoryAttributeModel productAttributeModel);
        bool Delete(int id);
    }
    public class ProductCategoryAttributeServices : IProductCategoryAttributeServices
    {
        private readonly IProductCategoryAttributeAppServices _productCategoryAttributeAppServices;
        private readonly IProductCategoryAppServices _productCategoryAppServices;
        public ProductCategoryAttributeServices(IProductCategoryAttributeAppServices productCategoryAttributeAppServices,
            IProductCategoryAppServices productCategoryAppServices)
        {
            _productCategoryAttributeAppServices = productCategoryAttributeAppServices;
            _productCategoryAppServices = productCategoryAppServices;
        }
        public int Create(ProductCategoryAttributeModel productAttributeModel)
        {
            ValidateRequestModel(productAttributeModel);            

            try
            {
                return _productCategoryAttributeAppServices.Create(MapToDto(productAttributeModel));
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotCreateResource, ex.Message);
            }
        }

        private void ValidateRequestModel(ProductCategoryAttributeModel productAttributeModel)
        {
            if (productAttributeModel == null)
                throw new BadRequestException(ApiErrorCode.CanNotCreateResource, "model can't be empty");

            if (productAttributeModel.ProductCategory == null || productAttributeModel.ProductCategory.CategoryId < 1)
                throw new BadRequestException(ApiErrorCode.CanNotCreateResource, "invalid product category");
        }

        public bool Delete(int id)
        {
            if (id < 1)
                throw new BadRequestException(ApiErrorCode.CanNotCreateResource, "invalid product category attribute id");

            try
            {
                var result = _productCategoryAttributeAppServices.Get(id);
                if (result == null)
                    throw new NotFoundException();

                return _productCategoryAttributeAppServices.Delete(id);
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotDeleteResource, ex.Message);
            }
        }

        public ProductCategoryAttributeModel Get(int id)
        {
            if (id < 1)
                throw new BadRequestException(ApiErrorCode.CanNotCreateResource, "invalid product category attribute id");

            try
            {
                var result = _productCategoryAttributeAppServices.Get(id);
                if (result == null)
                    throw new NotFoundException();

                return MapToResource(result);
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotGetResource, ex.Message);
            }
        }

        public IEnumerable<ProductCategoryAttributeModel> GetByProductCategoryId(int productCategoryId)
        {
            if (productCategoryId < 1)
                throw new BadRequestException(ApiErrorCode.CanNotCreateResource, "invalid product category id");

            try
            {
                var prodCategory = _productCategoryAppServices.Get(productCategoryId);
                
                if (prodCategory == null)
                    throw new NotFoundException();

                var result = _productCategoryAttributeAppServices.GetByProductCategoryId(productCategoryId);

                return result.Select(x => MapToResource(x));
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotGetResource, ex.Message);
            }
        }

        public int Update(int id, ProductCategoryAttributeModel productAttributeModel)
        {
            if (id < 1)
                throw new BadRequestException(ApiErrorCode.CanNotCreateResource, "invalid product category attribute id");

            ValidateRequestModel(productAttributeModel);

            try
            {
                var result = _productCategoryAttributeAppServices.Get(id);
                if (result == null)
                    throw new NotFoundException();

                return _productCategoryAttributeAppServices.Update(id, MapToDto(productAttributeModel));                
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotUpdateResource, ex.Message);
            }
        }

        private ProductCategoryAttributeModel MapToResource(ProductCategoryAttributeDto dto)
        {
            return new ProductCategoryAttributeModel()
            {
                AttributeId = dto.AttributeId,
                AttributeName = dto.AttributeName,
                ProductCategory = new ProductCategoryModel()
                {
                    CategoryId = dto.ProductCategory.CategoryId,
                    CategoryName = dto.ProductCategory.CategoryName
                }
            };
        }

        private ProductCategoryAttributeDto MapToDto(ProductCategoryAttributeModel productAttributeModel)
        {
            return new ProductCategoryAttributeDto()
            {
                AttributeId = productAttributeModel.AttributeId,
                AttributeName = productAttributeModel.AttributeName,
                ProductCategory = new ProductCategoryDto() { CategoryId = productAttributeModel.ProductCategory.CategoryId }
            };
        }
    }
}
