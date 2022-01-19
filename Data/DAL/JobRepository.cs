using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Data.DAL
{
    public class JobRepository : GenericRepository<Job>
    {
        private EventTypeRepository eventTypeRepository;
        private JobStatusRepository jobStatusRepository;
        private JobProductRepository jobProductRepository;

        public JobRepository(ApplicationDbContext context) : base(context)
        {
            eventTypeRepository = new EventTypeRepository(context);
            jobStatusRepository = new JobStatusRepository(context);
        }

        public Job CreateJob(Assignment assignment, DateTime deadline, IEnumerable<ItemProduct> products, ApplicationUser user)
        {
            if (assignment == null || deadline == null || products == null || user == null)
            {
                return null;
            }

            Job job = new Job()
            {
                Assignment = assignment,
                DueWhen = deadline,
                ETA = deadline.AddDays(-2),
                JobStatus = jobStatusRepository.GetByID((int)Enums.JobStatuses.JobCreated)
            };
            context.Jobs.Add(job);
            context.SaveChanges();

            foreach (ItemProduct itemProduct in products)
            {
                JobProduct jobProduct = new JobProduct()
                {
                    Product = itemProduct.Product,
                    NetPrice = itemProduct.NetPrice,
                    VAT = itemProduct.VAT,
                    Quantity = itemProduct.Quantity,
                    Job = job
                };
                context.JobProducts.Add(jobProduct);
            }
            context.SaveChanges();

            JobEventHistory newCreatedEvent = new JobEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.JobCreated),
                User = user,
                Job = job
            };
            job.EventHistory.Add(newCreatedEvent);

            context.SaveChanges();

            return job;
        }

        public Job CreateJobFromQuote(Quote quote, ApplicationUser user)
        {
            if (quote == null || user == null)
            {
                return null;
            }

            Job newJob = CreateJob(quote.Assignment, quote.DueWhen, quote.QuoteProducts, user);

            if (newJob == null)
            {
                return null;
            }

            newJob.Quote = quote;
            context.SaveChanges();

            return newJob;
        }

        public bool AssignTechnicianToJob(Job job, DateTime newEta, ApplicationUser technicianUser)
        {
            if (job == null || technicianUser == null || technicianUser.Technician == null)
            {
                return false;
            }

            if (job.JobStatus.ID != (int)Enums.JobStatuses.JobCreated)
            {
                return false;
            }

            JobEventHistory newPickupEvent = new JobEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.JobPickedUp),
                User = technicianUser,
                Job = job,
                Reason = string.Format("{0} picked up job, setting ETA as {1:dd/MM/yyyy HH:mm}", technicianUser.UserName, newEta)
            };
            job.EventHistory.Add(newPickupEvent);

            job.JobStatus = jobStatusRepository.GetByID((int)Enums.JobStatuses.PickedUp);
            job.ETA = newEta;

            technicianUser.Technician.CurrentJob = job;

            context.SaveChanges();

            return true;
        }

        public bool UpdateJobOnSite(Job job, ApplicationUser technicianUser)
        {
            if (job == null || technicianUser == null || technicianUser.Technician == null)
            {
                return false;
            }

            JobEventHistory newOnSiteEvent = new JobEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.JobTechnicianOnSite),
                User = technicianUser,
                Job = job
            };
            job.EventHistory.Add(newOnSiteEvent);

            job.JobStatus = jobStatusRepository.GetByID((int)Enums.JobStatuses.OnSite);

            context.SaveChanges();

            return true;
        }

        public bool UpdateJobETA(Job job, DateTime newEta, ApplicationUser user)
        {
            if (job == null || newEta == null || user == null)
            {
                return false;
            }

            JobEventHistory updateEtaEvent = new JobEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.JobEtaUpdated),
                User = user,
                Job = job,
                Reason = string.Format("ETA updated from {0:dd/MM/yyyy HH:mm} to {1:dd/MM/yyyy HH:mm}", job.ETA, newEta)
            };
            job.ETA = newEta;
            job.EventHistory.Add(updateEtaEvent);

            context.SaveChanges();

            return true;
        }

        public bool TechnicianDropJob(Job job, ApplicationUser technicianUser, string reason)
        {
            if (job == null || technicianUser == null || technicianUser.Technician == null || reason == null)
            {
                return false;
            }

            JobEventHistory newOnSiteEvent = new JobEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.JobTechnicianDropped),
                User = technicianUser,
                Job = job,
                Reason = reason
            };
            job.EventHistory.Add(newOnSiteEvent);

            job.JobStatus = jobStatusRepository.GetByID((int)Enums.JobStatuses.JobCreated);
            job.ETA = job.DueWhen.AddDays(-2);

            technicianUser.Technician.CurrentJob = null;

            context.SaveChanges();

            return true;
        }

        public bool TechnicianCompleteJob(Job job, ApplicationUser technicianUser)
        {
            if (job == null || technicianUser == null || technicianUser.Technician == null)
            {
                return false;
            }

            JobEventHistory newOnSiteEvent = new JobEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.JobTechnicianCompleted),
                User = technicianUser,
                Job = job
            };
            job.EventHistory.Add(newOnSiteEvent);

            job.JobStatus = jobStatusRepository.GetByID((int)Enums.JobStatuses.Completed);

            technicianUser.Technician.CurrentJob = null;

            context.SaveChanges();

            return true;
        }

        public bool RecallJob(Job job, string reason, ApplicationUser user)
        {
            if (job == null || user == null)
            {
                return false;
            }

            job.JobStatus = jobStatusRepository.GetByID((int)Enums.JobStatuses.JobCreated);

            JobEventHistory newOnSiteEvent = new JobEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.JobRecalled),
                User = user,
                Job = job,
                Reason = reason
            };
            job.EventHistory.Add(newOnSiteEvent);

            context.SaveChanges();

            return true;
        }

        public bool AddNoteToJob(Job job, string note, ApplicationUser user)
        {
            if (job == null || note == null || user == null)
            {
                return false;
            }

            JobNote newNote = new JobNote()
            {
                Job = job,
                WhenCreated = DateTime.Now,
                User = user,
                Text = note
            };
            context.JobNotes.Add(newNote);
            context.SaveChanges();

            return true;
        }
    }
}
