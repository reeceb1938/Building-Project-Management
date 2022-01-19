using NestLinkV2.Data;
using NestLinkV2.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.Settings.Data.DAL
{
    public class SettingsWorkUnit : IDisposable
    {
        private ApplicationDbContext _context;
        private ProductRepository productRepository;
        private ProductTypeRepository productTypeRepository;

        public ProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(_context);
                }
                return productRepository;
            }
        }

        public ProductTypeRepository ProductTypeRepository
        {
            get
            {
                if (productTypeRepository == null)
                {
                    productTypeRepository = new ProductTypeRepository(_context);
                }
                return productTypeRepository;
            }
        }

        public SettingsWorkUnit(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
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
