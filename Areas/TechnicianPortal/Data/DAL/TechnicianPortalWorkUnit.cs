using NestLinkV2.Data;
using NestLinkV2.Data.DAL;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NestLinkV2.Areas.TechnicianPortal.Data.DAL
{
    public class TechnicianPortalWorkUnit : IDisposable
    {
        private ApplicationDbContext _context;
        private TechnicianRepository technicianRepository;
        private JobRepository jobRepository;
        private QuoteRepository quoteRepository;
        private ProductRepository productRepository;

        public TechnicianRepository TechnicianRepository
        {
            get
            {
                if (technicianRepository == null)
                {
                    technicianRepository = new TechnicianRepository(_context);
                }
                return technicianRepository;
            }
        }

        public JobRepository JobRepository
        {
            get
            {
                if (jobRepository == null)
                {
                    jobRepository = new JobRepository(_context);
                }
                return jobRepository;
            }
        }

        public QuoteRepository QuoteRepository
        {
            get
            {
                if (quoteRepository == null)
                {
                    quoteRepository = new QuoteRepository(_context);
                }
                return quoteRepository;
            }
        }

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

        public TechnicianPortalWorkUnit(ApplicationDbContext context)
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

        public IEnumerable<Job> GetUnassignedJobs()
        {
             return JobRepository.Get(j => j.JobStatus.ID == (int)Enums.JobStatuses.JobCreated, q => q.OrderBy(j => j.DueWhen));
        }

        public IEnumerable<Quote> GetUnassignedQuotes()
        {
            return QuoteRepository.Get(q => q.QuoteStatus.ID == (int)Enums.QuoteStatuses.QuoteCreated, q => q.OrderBy(q => q.DueWhen));
        }
    }
}
