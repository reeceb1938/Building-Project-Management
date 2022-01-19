using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace NestLinkV2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Link DB Table to Model
            modelBuilder.Entity<Assignment>().ToTable("Assignments");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Complaint>().ToTable("Complaints");
            modelBuilder.Entity<EventType>().ToTable("EventTypes");
            modelBuilder.Entity<Job>().ToTable("Jobs");
            modelBuilder.Entity<JobEventHistory>().ToTable("JobEventHistories");
            modelBuilder.Entity<JobStatus>().ToTable("JobStatuses");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Quote>().ToTable("Quotes");
            modelBuilder.Entity<QuoteEventHistory>().ToTable("QuoteEventHistories");
            modelBuilder.Entity<Site>().ToTable("Sites");
            modelBuilder.Entity<WorkItem>().ToTable("WorkItems");
            modelBuilder.Entity<JobEventHistory>().ToTable("JobEventHistories");
            modelBuilder.Entity<QuoteEventHistory>().ToTable("QuoteEventHistories");
            modelBuilder.Entity<WorkItemEventHistory>().ToTable("WorkItemEventHistories");
            modelBuilder.Entity<ComplaintEventHistory>().ToTable("ComplaintEventHistories");
            modelBuilder.Entity<Technician>().ToTable("Technicians");
            modelBuilder.Entity<WorkItemObject>().ToTable("WorkItemObject");
            modelBuilder.Entity<QuoteType>().ToTable("QuoteTypes");
            modelBuilder.Entity<QuoteStatus>().ToTable("QuoteStatuses");
            modelBuilder.Entity<ClientUser>().ToTable("ClientUsers");
            modelBuilder.Entity<ProductType>().ToTable("ProductTypes");
            modelBuilder.Entity<JobProduct>().ToTable("JobProducts");
            modelBuilder.Entity<QuoteProduct>().ToTable("QuoteProducts");
            modelBuilder.Entity<JobNote>().ToTable("JobNotes");
            modelBuilder.Entity<QuoteNote>().ToTable("QuoteNotes");

            // Seed data
            seedJobStatus(modelBuilder);
            seedEventType(modelBuilder);
            seedQuoteStatus(modelBuilder);
            seedQuoteTypes(modelBuilder);
            seedProductType(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintEventHistory> ComplaintEventHistories { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobEventHistory> JobEventHistories { get; set; }
        public DbSet<JobStatus> JobStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteEventHistory> QuoteEventHistories { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<WorkItemEventHistory> WorkItemEventHistories { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<WorkItemObject> WorkItemObjects { get; set; }
        public DbSet<QuoteType> QuoteTypes { get; set; }
        public DbSet<QuoteStatus> QuoteStatuses { get; set; }
        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<JobProduct> JobProducts { get; set; }
        public DbSet<QuoteProduct> QuoteProducts { get; set; }
        public DbSet<JobNote> JobNotes { get; set; }
        public DbSet<QuoteNote> QuoteNotes { get; set; }

        private void seedJobStatus(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobStatus>().HasData(
                new JobStatus
                {
                    ID = (int)Enums.JobStatuses.JobCreated,
                    Name = "Job Created"
                },
                new JobStatus
                {
                    ID = (int)Enums.JobStatuses.PickedUp,
                    Name = "Picked Up"
                },
                new JobStatus
                {
                    ID = (int)Enums.JobStatuses.OnSite,
                    Name = "On Site"
                },
                new JobStatus
                {
                    ID = (int)Enums.JobStatuses.Completed,
                    Name = "Completed"
                }
            );
        }

        private void seedEventType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventType>().HasData(
                new EventType
                {
                    ID = (int)Enums.EventTypes.JobCreated,
                    Name = "Job Created"
                },
                new EventType
                {
                    ID = (int)Enums.EventTypes.JobPickedUp,
                    Name = "Job Picked Up"
                },
                new EventType
                {
                    ID = (int)Enums.EventTypes.JobTechnicianOnSite,
                    Name = "Technician On Site"
                },
                new EventType
                {
                    ID = (int)Enums.EventTypes.JobTechnicianDropped,
                    Name = "Technician Dropped Job"
                },
                new EventType
                {
                    ID = (int)Enums.EventTypes.JobTechnicianCompleted,
                    Name = "Technician Completed Job"
                },
                new EventType
                {
                    ID = (int)Enums.EventTypes.QuoteCreated,
                    Name = "Quote Created"
                },
                new EventType
                {
                    ID = (int)Enums.EventTypes.QuoteSurveyScheduled,
                    Name = "Quote Survey Scheduled"
                },
                new EventType
                {
                    ID = (int)Enums.EventTypes.QuoteSurveyed,
                    Name = "Quote Surveyed"
                },
                new EventType
                {
                    ID = (int)Enums.EventTypes.QuoteApproved,
                    Name = "Quote Approved"
                },
                new EventType
                {
                    ID = (int)Enums.EventTypes.QuoteDeclined,
                    Name = "Quote Declined"
                },
                new EventType
                {
                    ID = (int)Enums.EventTypes.JobEtaUpdated,
                    Name = "ETA Updated"
                },
                new EventType
                {
                    ID = (int)Enums.EventTypes.JobRecalled,
                    Name = "Job Recalled"
                }
            );
        }

        private void seedQuoteTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuoteType>().HasData(
                new QuoteType
                {
                    ID = (int)Enums.QuoteTypes.DesktopQuote,
                    Name = "Desktop Quote"
                },
                new QuoteType
                {
                    ID = (int)Enums.QuoteTypes.SurveyQuote,
                    Name = "Survey Quote"
                }
            );
        }

        private void seedQuoteStatus(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuoteStatus>().HasData(
                new QuoteStatus
                {
                    ID = (int)Enums.QuoteStatuses.QuoteCreated,
                    Name = "Quote Created"
                },
                new QuoteStatus
                {
                    ID = (int)Enums.QuoteStatuses.SurveyScheduled,
                    Name = "Survey Scheduled"
                },
                new QuoteStatus
                {
                    ID = (int)Enums.QuoteStatuses.AwaitingApproval,
                    Name = "Awaiting Approval"
                },
                new QuoteStatus
                {
                    ID = (int)Enums.QuoteStatuses.QuoteApproved,
                    Name = "Quote Approved"
                },
                new QuoteStatus
                {
                    ID = (int)Enums.QuoteStatuses.QuoteDeclined,
                    Name = "Quote Declined"
                }
            );
        }

        private void seedProductType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType
                {
                    ID = (int)Enums.ProductTypes.Product,
                    Name = "Product"
                },
                new ProductType
                {
                    ID = (int)Enums.ProductTypes.Part,
                    Name = "Part"
                },
                new ProductType
                {
                    ID = (int)Enums.ProductTypes.Labour,
                    Name = "Labour"
                }
            );
        }
    }
}
