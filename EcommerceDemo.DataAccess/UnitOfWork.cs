using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using EcommerceDemo.DataAccess.Domain;

namespace EcommerceDemo.DataAccess
{
    public interface IUnitOfWork
    {
        GenericRepository<Product> ProductRepository { get; }
        GenericRepository<ProductAttribute> ProductAttributeRepository { get; }
        GenericRepository<ProductAttributeLookup> ProductCategoryAttributeRepository { get; }
        GenericRepository<ProductCategory> ProductCategoryRepository { get; }
        void Save();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDemoEntities _context;        
        private GenericRepository<Product> _productRepository;
        private GenericRepository<ProductAttribute> _productAttributeRepository;
        private GenericRepository<ProductAttributeLookup> _productCategoryAttributeRepository;
        private GenericRepository<ProductCategory> _productCategoryRepository;

        public UnitOfWork(ECommerceDemoEntities context)
        {
            _context = context;
        }
        /// <summary>
        /// Get/Set Property for product repository.
        /// </summary>
        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                    this._productRepository = new GenericRepository<Product>(_context);
                return _productRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for product attribute repository.
        /// </summary>
        public GenericRepository<ProductAttribute> ProductAttributeRepository
        {
            get
            {
                if (this._productAttributeRepository == null)
                    this._productAttributeRepository = new GenericRepository<ProductAttribute>(_context);
                return _productAttributeRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for product category repository.
        /// </summary>
        public GenericRepository<ProductCategory> ProductCategoryRepository
        {
            get
            {
                if (this._productCategoryRepository == null)
                    this._productCategoryRepository = new GenericRepository<ProductCategory>(_context);
                return _productCategoryRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for product attribute lookup repository.
        /// </summary>
        public GenericRepository<ProductAttributeLookup> ProductCategoryAttributeRepository
        {
            get
            {
                if (this._productCategoryAttributeRepository == null)
                    this._productCategoryAttributeRepository = new GenericRepository<ProductAttributeLookup>(_context);
                return _productCategoryAttributeRepository;
            }
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _context.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
