using EcommerceDemo.Api.ResourceModel;
using EcommerceDemo.Api.ResourceModel.Exceptions;
using EcommerceDemo.Core.DtoModel;
using EcommerceDemo.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceDemo.Api.Services
{
    public interface IProductServices
    {
        IEnumerable<ProductModel> GetAll(int limit, int offset, string sortBy, string sortDir, string searchTerm);
        ProductModel Get(long id);
        long Create(ProductModel model);
        long Update(long id, ProductModel model);
        bool Delete(long id);
    }

    public class ProductServices : IProductServices
    {
        private readonly IProductAppServices _productAppServices;

        public ProductServices(IProductAppServices productAppServices)
        {
            _productAppServices = productAppServices;
        }

        public IEnumerable<ProductModel> GetAll(int limit, int offset, string sortBy, string sortDir, string searchTerm)
        {
            try
            {
                var result = _productAppServices.GetAll(limit, offset, sortBy, sortDir, searchTerm);                
                return result.Select(x => MapToResource(x));
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotGetResource, ex.Message);
            }
        }

        public ProductModel Get(long id)
        {
            if (id < 1)
                throw new BadRequestException(ApiErrorCode.CanNotGetResource, "invalid product  id");

            try
            {
                var result = _productAppServices.Get(id);

                if (result == null)
                    throw new NotFoundException();

                return MapToResource(result);
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotGetResource, ex.Message);
            }

        }

        public long Create(ProductModel model)
        {
            ValidateRequestModel(model);

            try
            {
                return _productAppServices.Create(MapToDto(model));                
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotGetResource, ex.Message);
            }
        }

        private void ValidateRequestModel(ProductModel model)
        {
            if (model == null)
                throw new BadRequestException(ApiErrorCode.CanNotCreateResource, "model can't be empty");
        }

        public long Update(long id, ProductModel model)
        {
            if (id < 1)
                throw new BadRequestException(ApiErrorCode.CanNotUpdateResource, "invalid product id");

            ValidateRequestModel(model);

            try
            {
                var result = _productAppServices.Get(id);

                if (result == null)
                    throw new NotFoundException();

                return _productAppServices.Update(id, MapToDto(model));                
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotGetResource, ex.Message);
            }
        }

        public bool Delete(long id)
        {
            if (id < 1)
                throw new BadRequestException(ApiErrorCode.CanNotDeleteResource, "invalid product id");

            try
            {
                var result = _productAppServices.Get(id);

                if (result == null)
                    throw new NotFoundException();

                return _productAppServices.Delete(id);
            }
            catch (InvalidServiceCallException ex)
            {
                throw new ForbiddenException(ApiErrorCode.CanNotGetResource, ex.Message);
            }
        }

        private ProductDto MapToDto(ProductModel model)
        {
            return new ProductDto()
            {
                ProductId = model.ProductId,
                ProductName = model.ProductName,
                ProductDescription = model.ProductDescription,
                Category = model.Category != null ? new ProductCategoryDto()
                {
                    CategoryId = model.Category.CategoryId,
                    CategoryName = model.Category.CategoryName
                } : null,
                Attributes = model.Attributes?.Select(x => new ProductAttributeDto()
                {
                    AttributeId = x.AttributeId,
                    AttributeValue = x.AttributeValue
                }) ?? new List<ProductAttributeDto>()
            };
        }

        private ProductModel MapToResource(ProductDto productDto)
        {
            return new ProductModel()
            {
                ProductId = productDto.ProductId,
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                Category = productDto.Category != null ? new ProductCategoryModel()
                {
                    CategoryId = productDto.Category.CategoryId,
                    CategoryName = productDto.Category.CategoryName
                } : null,
                Attributes = productDto.Attributes?.Select(x => new ProductAttributeModel() { AttributeId = x.AttributeId, AttributeValue = x.AttributeValue }) 
                ?? new List <ProductAttributeModel>()
            };
        }
    }
}
