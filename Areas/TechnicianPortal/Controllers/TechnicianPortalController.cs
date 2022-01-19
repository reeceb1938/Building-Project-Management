using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NestLinkV2.Data;
using Microsoft.AspNetCore.Authorization;
using NestLinkV2.Models;
using NestLinkV2.Areas.TechnicianPortal.ViewModels;
using NestLinkV2.Areas.TechnicianPortal.Data.DAL;
using NestLinkV2.ViewModels;
using Newtonsoft.Json;

namespace NestLinkV2.Areas.TechnicianPortal.Controllers
{
    [Area("TechnicianPortal")]
    [Authorize(Roles = "Technician")]
    public class TechnicianPortalController : Controller
    {
        private readonly ILogger<TechnicianPortalController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private TechnicianPortalWorkUnit _workUnit;

        public TechnicianPortalController(ILogger<TechnicianPortalController> logger, UserManager<ApplicationUser> userManager, TechnicianPortalWorkUnit workUnit)
        {
            _logger = logger;
            _userManager = userManager;
            _workUnit = workUnit;
        }

        public IActionResult Index()
        {
            ApplicationUser currentUser = _userManager.GetUserAsync(User).Result;

            if (currentUser.Technician == null)
            {
                _logger.LogInformation(string.Format("{0} (ID: {1}) has role technician but is not in technicians table. Adding them now", currentUser.UserName, currentUser.Id));
                currentUser.Technician = new Technician
                {
                    User = currentUser
                };
                _workUnit.TechnicianRepository.Insert(currentUser.Technician);
                _workUnit.Save();
            }

            TechnicianDashboardViewModel techDashViewModel = new TechnicianDashboardViewModel();
            techDashViewModel.Technician = currentUser.Technician;

            return View(techDashViewModel);
        }

        public IActionResult JobPickList()
        {
            JobPickListViewModel jobPickListViewModel = new JobPickListViewModel()
            {
                Jobs = _workUnit.GetUnassignedJobs().ToList()
            };

            return View(jobPickListViewModel);
        }

        public IActionResult ViewJob(int id)
        {
            Job job = _workUnit.JobRepository.GetByID(id);

            if (job == null)
            {
                return RedirectToAction("Index");
            }

            ViewJobViewModel viewJobViewModel = new ViewJobViewModel()
            {
                User = _userManager.GetUserAsync(User).Result,
                Job = job
            };

            return View(viewJobViewModel);
        }

        [HttpGet]
        public IActionResult PickUpJob(int id)
        {
            Job job = _workUnit.JobRepository.GetByID(id);

            if (job == null)
            {
                return RedirectToAction("Index");
            }

            TechnicianPickupJobViewModel pickupJobViewModel = new TechnicianPickupJobViewModel()
            {
                JobID = job.ID,
                ETA = DateTime.Now.AddHours(2)
            };

            return View(pickupJobViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PickUpJob(TechnicianPickupJobViewModel pickupJobViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(pickupJobViewModel);
            }
            else
            {
                Job job = _workUnit.JobRepository.GetByID(pickupJobViewModel.JobID);
                ApplicationUser user = _userManager.GetUserAsync(User).Result;

                if (job == null || user == null || user.Technician == null)
                {
                    return RedirectToAction("Index");
                }

                if (_workUnit.JobRepository.AssignTechnicianToJob(job, pickupJobViewModel.ETA, user))
                {
                    return RedirectToAction("ViewJob", new { id = job.ID });
                }

                return RedirectToAction("Index");
            }
        }

        public IActionResult UpdateJobOnSite(int id)
        {
            Job job = _workUnit.JobRepository.GetByID(id);
            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            if (job == null || user == null || user.Technician == null)
            {
                return RedirectToAction("Index");
            }

            if (job.ID != user.Technician.CurrentJob.ID)
            {
                _logger.LogError(string.Format("User {0} ({1}) tried to set onsite for job {0} but is not the current technician"), user.UserName, user.Id, job.ID);
                return RedirectToAction("Index");
            }

            if (_workUnit.JobRepository.UpdateJobOnSite(job, user))
            {
                return RedirectToAction("ViewJob", new { id = id });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateJobETA(int id)
        {
            Job job = _workUnit.JobRepository.GetByID(id);
            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            if (job == null || user == null || user.Technician == null)
            {
                return RedirectToAction("Index");
            }

            if (job.ID != user.Technician.CurrentJob.ID)
            {
                _logger.LogError(string.Format("User {0} ({1}) tried to update ETA for job {0} but is not the current technician"), user.UserName, user.Id, job.ID);
                return RedirectToAction("Index");
            }

            TechnicianUpdateJobETAViewModel updateEtaViewModel = new TechnicianUpdateJobETAViewModel()
            {
                JobID = job.ID,
                ETA = job.ETA
            };

            return View(updateEtaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateJobETA(TechnicianUpdateJobETAViewModel updateEtaViewModel)
        {
            Job job = _workUnit.JobRepository.GetByID(updateEtaViewModel.JobID);
            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            if (job == null || user == null || user.Technician == null)
            {
                return RedirectToAction("Index");
            }

            if (job.ID != user.Technician.CurrentJob.ID)
            {
                _logger.LogError(string.Format("User {0} ({1}) tried to update ETA for job {0} but is not the current technician"), user.UserName, user.Id, job.ID);
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(updateEtaViewModel);
            }
            else
            {
                if (_workUnit.JobRepository.UpdateJobETA(job, updateEtaViewModel.ETA, user))
                {
                    return RedirectToAction("ViewJob", new { id = job.ID });
                }

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult DropJob(int id)
        {
            Job job = _workUnit.JobRepository.GetByID(id);

            if (job == null)
            {
                return RedirectToAction("Index");
            }

            TechnicianDropJobViewModel technicianDropJobViewModel = new TechnicianDropJobViewModel() { 
                JobID = job.ID
            };

            return View(technicianDropJobViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DropJob(TechnicianDropJobViewModel technicianDropJobViewModel)
        {
            if (ModelState.IsValid)
            {
                Job job = _workUnit.JobRepository.GetByID(technicianDropJobViewModel.JobID);
                ApplicationUser user = _userManager.GetUserAsync(User).Result;

                if (job == null || user == null || user.Technician == null)
                {
                    return RedirectToAction("Index");
                }

                if (job.ID != user.Technician.CurrentJob.ID)
                {
                    _logger.LogError(string.Format("User {0} ({1}) tried to drop job {0} but is not the current technician"), user.UserName, user.Id, job.ID);
                    return RedirectToAction("Index");
                }

                if (_workUnit.JobRepository.TechnicianDropJob(job, user, technicianDropJobViewModel.Reason))
                {
                    return RedirectToAction("ViewJob", new { id = job.ID });
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult CompleteJob(int id)
        {
            Job job = _workUnit.JobRepository.GetByID(id);
            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            if (job == null || user == null || user.Technician == null)
            {
                return RedirectToAction("Index");
            }

            if (job.ID != user.Technician.CurrentJob.ID)
            {
                _logger.LogError(string.Format("User {0} ({1}) tried to complete job {0} but is not the current technician"), user.UserName, user.Id, job.ID);
                return RedirectToAction("Index");
            }

            if (_workUnit.JobRepository.TechnicianCompleteJob(job, user))
            {
                return RedirectToAction("ViewJob", new { id = id });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddNoteToJob(int id)
        {
            Job job = _workUnit.JobRepository.GetByID(id);

            if (job == null)
            {
                return RedirectToAction("Index");
            }

            JobAddNoteViewModel jobAddNoteViewModel = new JobAddNoteViewModel()
            {
                JobID = job.ID
            };

            return View(jobAddNoteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNoteToJob(JobAddNoteViewModel jobAddNoteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(jobAddNoteViewModel);
            }
            else
            {
                Job job = _workUnit.JobRepository.GetByID(jobAddNoteViewModel.JobID);
                ApplicationUser user = _userManager.GetUserAsync(User).Result;

                if (job == null || user == null)
                {
                    return RedirectToAction("Index");
                }

                if (_workUnit.JobRepository.AddNoteToJob(job, jobAddNoteViewModel.Text, user))
                {
                    return RedirectToAction("ViewJob", new { job.ID });
                }

                return RedirectToAction("Index");
            }
        }

        public IActionResult QuotePickList()
        {
            QuotePickListViewModel quotePickListViewModel = new QuotePickListViewModel()
            {
                Quotes = _workUnit.GetUnassignedQuotes().ToList()
            };

            return View(quotePickListViewModel);
        }

        public IActionResult ViewQuote(int id)
        {
            Quote quote = _workUnit.QuoteRepository.GetByID(id);

            if (quote == null)
            {
                return RedirectToAction("Index");
            }

            ViewQuoteViewModel viewQuoteViewModel = new ViewQuoteViewModel()
            {
                User = _userManager.GetUserAsync(User).Result,
                Quote = quote
            };

            return View(viewQuoteViewModel);
        }

        [HttpGet]
        public IActionResult ScheduleQuoteSurvey(int id)
        {
            Quote quote = _workUnit.QuoteRepository.GetByID(id);

            if (quote == null)
            {
                return RedirectToAction("Index");
            }

            TechnicianScheduleQuoteSurveyViewModel scheduleQuoteSurveyViewModel = new TechnicianScheduleQuoteSurveyViewModel()
            {
                QuoteID = quote.ID,
                AppointmentDateTime = DateTime.Now.AddDays(1)
            };

            return View(scheduleQuoteSurveyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ScheduleQuoteSurvey(TechnicianScheduleQuoteSurveyViewModel scheduleQuoteSurveyViewModel)
        {
            if (ModelState.IsValid)
            {
                Quote quote = _workUnit.QuoteRepository.GetByID(scheduleQuoteSurveyViewModel.QuoteID);
                ApplicationUser user = _userManager.GetUserAsync(User).Result;

                if (quote == null || user == null || user.Technician == null)
                {
                    return RedirectToAction("Index");
                }

                if (_workUnit.QuoteRepository.ScheduleQuoteSurvey(quote, user, scheduleQuoteSurveyViewModel.AppointmentDateTime))
                {
                    return RedirectToAction("ViewQuote", new { id = quote.ID });
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CompleteQuoteSurvey(int id)
        {
            Quote quote = _workUnit.QuoteRepository.GetByID(id);
            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            if (quote == null || user == null || user.Technician == null || quote.QuoteAppointment == null || quote.QuoteAppointment.Technician.ID != user.Technician.ID)
            {
                return RedirectToAction("Index");
            }

            CompleteQuoteSurveyViewModel completeQuoteSurveyViewModel = new CompleteQuoteSurveyViewModel()
            {
                QuoteID = quote.ID,
                Products = _workUnit.ProductRepository.GetProductSelectList(),
                QuoteProducts = quote.QuoteProducts
            };

            return View(completeQuoteSurveyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteQuoteSurvey(CompleteQuoteSurveyViewModel completeQuoteSurveyViewModel)
        {
            Quote quote = _workUnit.QuoteRepository.GetByID(completeQuoteSurveyViewModel.QuoteID);

            if (quote == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                completeQuoteSurveyViewModel.QuoteProducts = quote.QuoteProducts;
                completeQuoteSurveyViewModel.Products = _workUnit.ProductRepository.GetProductSelectList();

                return View(completeQuoteSurveyViewModel);
            }
            else
            {
                ApplicationUser user = _userManager.GetUserAsync(User).Result;

                if (user == null || user.Technician == null || quote.QuoteAppointment == null || quote.QuoteAppointment.Technician.ID != user.Technician.ID)
                {
                    return RedirectToAction("Index");
                }

                // Parse component list JSON
                List<ItemProductViewModel> parsedProductList;
                List<QuoteProduct> quoteProductList = new List<QuoteProduct>();

                try
                {
                    parsedProductList = JsonConvert.DeserializeObject<List<ItemProductViewModel>>(completeQuoteSurveyViewModel.ComponentListJSON);
                }
                catch (JsonException)
                {
                    return RedirectToAction("Index");
                }

                foreach (ItemProductViewModel itemProductViewModel in parsedProductList)
                {
                    Product product = _workUnit.ProductRepository.GetByID(itemProductViewModel.ProductID);

                    if (product == null)
                    {
                        continue;
                    }

                    quoteProductList.Add(new QuoteProduct()
                    {
                        Product = product,
                        NetPrice = itemProductViewModel.NetPrice,
                        VAT = itemProductViewModel.VAT,
                        Quantity = itemProductViewModel.Quantity
                    });
                }

                if (_workUnit.QuoteRepository.CompleteQuoteSurvey(quote, quoteProductList, user))
                {
                    return RedirectToAction("ViewQuote", new { id = quote.ID });
                }
            }

            completeQuoteSurveyViewModel.QuoteProducts = quote.QuoteProducts;
            completeQuoteSurveyViewModel.Products = _workUnit.ProductRepository.GetProductSelectList();

            return View(completeQuoteSurveyViewModel);
        }

        [HttpGet]
        public IActionResult AddNoteToQuote(int id)
        {
            Quote quote = _workUnit.QuoteRepository.GetByID(id);

            if (quote == null)
            {
                return RedirectToAction("Index");
            }

            QuoteAddNoteViewModel quoteAddNoteViewModel = new QuoteAddNoteViewModel()
            {
                QuoteID = quote.ID
            };

            return View(quoteAddNoteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNoteToQuote(QuoteAddNoteViewModel quoteAddNoteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(quoteAddNoteViewModel);
            }
            else
            {
                Quote quote = _workUnit.QuoteRepository.GetByID(quoteAddNoteViewModel.QuoteID);
                ApplicationUser user = _userManager.GetUserAsync(User).Result;

                if (quote == null || user == null)
                {
                    return RedirectToAction("Index");
                }

                if (_workUnit.QuoteRepository.AddNoteToQuote(quote, quoteAddNoteViewModel.Text, user))
                {
                    return RedirectToAction("ViewQuote", new { quote.ID });
                }

                return RedirectToAction("Index");
            }
        }
    }
}
