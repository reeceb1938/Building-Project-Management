using Microsoft.EntityFrameworkCore;
using NestLinkV2.Data;
using NestLinkV2.Data.DAL;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.ClientPortal.Data
{
    public class ClientPortalWorkUnit : IDisposable
    {
        private ApplicationDbContext _context;
        private QuoteRepository quoteRepository;
        private JobRepository jobRepository;
        private QuoteStatusRepository quoteStatusRepository;
        private JobStatusRepository jobStatusRepository;

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

        public QuoteStatusRepository QuoteStatusRepository
        {
            get
            {
                if (quoteStatusRepository == null)
                {
                    quoteStatusRepository = new QuoteStatusRepository(_context);
                }
                return quoteStatusRepository;
            }
        }

        public JobStatusRepository JobStatusRepository
        {
            get
            {
                if (jobStatusRepository == null)
                {
                    jobStatusRepository = new JobStatusRepository(_context);
                }
                return jobStatusRepository;
            }
        }

        public ClientPortalWorkUnit(ApplicationDbContext context)
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

        public IEnumerable<Job> GetClientActiveJobs(Client client)
        {
            if (client == null)
            {
                return null;
            }

            FormattableString queryString = $@"
                SELECT
	                Jobs.*
                FROM
	                Jobs
                LEFT OUTER JOIN Assignments ON Jobs.AssignmentId = Assignments.ID
                LEFT OUTER JOIN Sites ON Assignments.SiteId = Sites.ID
                LEFT Outer JOIN Clients ON Sites.ClientId = Clients.ID
                WHERE
	                Clients.ID = {client.ID}
                    
            ";

            IEnumerable<Job> activeJobs = _context.Jobs.FromSqlInterpolated(queryString);

            return activeJobs;
        }

        public IEnumerable<Quote> GetClientPendingQuotes(Client client)
        {
            if (client == null)
            {
                return null;
            }

            QuoteStatus awaitingApprovalStatus = QuoteStatusRepository.GetByID((int)Enums.QuoteStatuses.AwaitingApproval);

            if (awaitingApprovalStatus == null)
            {
                return null;
            }

            FormattableString queryString = $@"
                SELECT
	                Quotes.*
                FROM
	                Quotes
                LEFT OUTER JOIN Assignments ON Quotes.AssignmentId = Assignments.ID
                LEFT OUTER JOIN Sites ON Assignments.SiteId = Sites.ID
                LEFT Outer JOIN Clients ON Sites.ClientId = Clients.ID
                WHERE
	                Clients.ID = {client.ID}
                    
            ";

            IEnumerable<Quote> pendingQuotes = _context.Quotes.FromSqlInterpolated(queryString);

            return pendingQuotes;
        }
    }
}
