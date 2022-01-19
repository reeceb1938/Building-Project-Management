using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Data.DAL
{
    public class QuoteRepository : GenericRepository<Quote>
    {
        private EventTypeRepository eventTypeRepository;
        private QuoteTypeRepository quoteTypeRepository;
        private QuoteStatusRepository quoteStatusRepository;
        private QuoteProductRepository quoteProductRepository;
        private JobRepository jobRepository;

        public QuoteRepository(ApplicationDbContext context) : base(context)
        {
            eventTypeRepository = new EventTypeRepository(context);
            quoteTypeRepository = new QuoteTypeRepository(context);
            quoteStatusRepository = new QuoteStatusRepository(context);
            quoteProductRepository = new QuoteProductRepository(context);
            jobRepository = new JobRepository(context);
        }

        public Quote CreateQuote(Assignment assignment, QuoteType quoteType, DateTime deadline, IEnumerable<ItemProduct> products, ApplicationUser user)
        {
            if (assignment == null || quoteType == null || deadline == null || products == null || user == null)
            {
                return null;
            }

            Quote quote = new Quote()
            {
                Assignment = assignment,
                QuoteType = quoteType,
                QuoteStatus = quoteStatusRepository.GetByID((int)Enums.QuoteStatuses.QuoteCreated),
                DueWhen = deadline,
            };
            context.Quotes.Add(quote);
            context.SaveChanges();

            foreach (ItemProduct itemProduct in products)
            {
                QuoteProduct quoteProduct = new QuoteProduct()
                {
                    Product = itemProduct.Product,
                    NetPrice = itemProduct.NetPrice,
                    VAT = itemProduct.VAT,
                    Quantity = itemProduct.Quantity,
                    Quote = quote
                };
                context.QuoteProducts.Add(quoteProduct);
            }
            context.SaveChanges();

            QuoteEventHistory newCreatedEvent = new QuoteEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.QuoteCreated),
                User = user,
                Quote = quote
            };
            quote.EventHistory.Add(newCreatedEvent);

            context.SaveChanges();

            return quote;
        }

        public bool ScheduleQuoteSurvey(Quote quote, ApplicationUser technicianUser, DateTime surveyDateTime)
        {
            if (quote == null || technicianUser == null || technicianUser.Technician == null || surveyDateTime == null)
            {
                return false;
            }

            if (quote.QuoteStatus.ID != (int)Enums.QuoteStatuses.QuoteCreated)
            {
                return false;
            }

            QuoteEventHistory newSurveyScheduledEvent = new QuoteEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.QuoteSurveyScheduled),
                User = technicianUser,
                Quote = quote
            };
            quote.EventHistory.Add(newSurveyScheduledEvent);

            QuoteAppointment appointment = new QuoteAppointment()
            {
                Quote = quote,
                Technician = technicianUser.Technician,
                When = surveyDateTime
            };
            quote.QuoteAppointment = appointment;

            quote.QuoteStatus = quoteStatusRepository.GetByID((int)Enums.QuoteStatuses.SurveyScheduled);

            context.SaveChanges();

            return true;
        }

        public bool CompleteQuoteSurvey(Quote quote, IEnumerable<ItemProduct> newProductList, ApplicationUser technicianUser)
        {
            if (quote == null || newProductList == null || technicianUser == null)
            {
                return false;
            }

            foreach (QuoteProduct quoteProduct in quote.QuoteProducts)
            {
                quoteProductRepository.Delete(quoteProduct.ID);
            }

            foreach (ItemProduct itemProduct in newProductList)
            {
                QuoteProduct quoteProduct = new QuoteProduct()
                {
                    Product = itemProduct.Product,
                    NetPrice = itemProduct.NetPrice,
                    VAT = itemProduct.VAT,
                    Quantity = itemProduct.Quantity,
                    Quote = quote
                };
                context.QuoteProducts.Add(quoteProduct);
            }

            QuoteEventHistory newCreatedEvent = new QuoteEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.QuoteSurveyed),
                User = technicianUser,
                Quote = quote
            };
            quote.EventHistory.Add(newCreatedEvent);

            quote.QuoteStatus = quoteStatusRepository.GetByID((int)Enums.QuoteStatuses.AwaitingApproval);

            context.SaveChanges();

            return true;
        }

        public bool ClientApproveQuote(Quote quote, ApplicationUser clientUser)
        {
            if (quote == null || clientUser == null || clientUser.ClientUser == null)
            {
                return false;
            }

            if (quote.Assignment.Site.Client == null)
            {
                return false;
            }

            if (clientUser.ClientUser.Client.ID != quote.Assignment.Site.Client.ID)
            {
                return false;
            }

            QuoteEventHistory clientApproveQuoteEvent = new QuoteEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.QuoteApproved),
                User = clientUser,
                Quote = quote
            };
            quote.EventHistory.Add(clientApproveQuoteEvent);

            quote.QuoteStatus = quoteStatusRepository.GetByID((int)Enums.QuoteStatuses.QuoteApproved);

            context.SaveChanges();

            return jobRepository.CreateJobFromQuote(quote, clientUser) != null;
        }

        public bool ClientDeclineQuote(Quote quote, ApplicationUser clientUser)
        {
            if (quote == null || clientUser == null || clientUser.ClientUser == null)
            {
                return false;
            }

            if (quote.Assignment.Site.Client == null)
            {
                return false;
            }

            if (clientUser.ClientUser.Client.ID != quote.Assignment.Site.Client.ID)
            {
                return false;
            }

            QuoteEventHistory clientApproveQuoteEvent = new QuoteEventHistory()
            {
                EventType = eventTypeRepository.GetByID((int)Enums.EventTypes.QuoteDeclined),
                User = clientUser,
                Quote = quote
            };
            quote.EventHistory.Add(clientApproveQuoteEvent);

            quote.QuoteStatus = quoteStatusRepository.GetByID((int)Enums.QuoteStatuses.QuoteDeclined);

            context.SaveChanges();

            return true;
        }

        public bool AddNoteToQuote(Quote quote, string note, ApplicationUser user)
        {
            if (quote == null || note == null || user == null)
            {
                return false;
            }

            QuoteNote newNote = new QuoteNote()
            {
                Quote = quote,
                WhenCreated = DateTime.Now,
                User = user,
                Text = note
            };
            context.QuoteNotes.Add(newNote);
            context.SaveChanges();

            return true;
        }
    }
}
