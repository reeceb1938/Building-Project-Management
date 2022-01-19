using Microsoft.AspNetCore.Mvc.Rendering;
using NestLinkV2.Data;
using NestLinkV2.Data.DAL;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.AdminStaffPortal.Data.DAL
{
    public class AdminStaffPortalWorkUnit : IDisposable
    {
        private ApplicationDbContext _context;
        private QuoteRepository quoteRepository;
        private QuoteTypeRepository quoteTypeRepository;
        private JobRepository jobRepository;
        private ClientRepository clientRepository;
        private SiteRepository siteRepository;
        private AssignmentRepository assignmentRepository;
        private ProductRepository productRepository;
        private ComplaintRepository complaintRepository;

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

        public QuoteTypeRepository QuoteTypeRepository
        {
            get
            {
                if (quoteTypeRepository == null)
                {
                    quoteTypeRepository = new QuoteTypeRepository(_context);
                }
                return quoteTypeRepository;
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

        public AssignmentRepository AssignmentRepository
        {
            get
            {
                if (assignmentRepository == null)
                {
                    assignmentRepository = new AssignmentRepository(_context);
                }
                return assignmentRepository;
            }
        }

        public ClientRepository ClientRepository
        {
            get
            {
                if (clientRepository == null)
                {
                    clientRepository = new ClientRepository(_context);
                }
                return clientRepository;
            }
        }

        public SiteRepository SiteRepository
        {
            get
            {
                if (siteRepository == null)
                {
                    siteRepository = new SiteRepository(_context);
                }
                return siteRepository;
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

        public ComplaintRepository ComplaintRepository
        {
            get
            {
                if (complaintRepository == null)
                {
                    complaintRepository = new ComplaintRepository(_context);
                }
                return complaintRepository;
            }
        }

        public AdminStaffPortalWorkUnit(ApplicationDbContext context)
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

        public IEnumerable<SelectListItem> GetSiteSelectList()
        {
            IEnumerable<SelectListItem> results = _context.Sites.Select(a => new SelectListItem()
            {
                Value = a.ID.ToString(),
                Text = string.Format("{0}, {1} ({2})", a.AddressLine1, a.Postcode, a.Client == null ? "Independent Site" : a.Client.Name)
            });

            return results;
        }
    }
}
