using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NestLinkV2.Areas.AdminStaffPortal.Data.DAL;
using NestLinkV2.Areas.AdminStaffPortal.ViewModels;
using NestLinkV2.Models;
using NestLinkV2.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.AdminStaffPortal.Controllers
{
    [Area("AdminStaffPortal")]
    [Authorize(Roles = "AdminStaff")]
    public class AdminStaffPortalController : Controller
    {
        private readonly ILogger<AdminStaffPortalController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private AdminStaffPortalWorkUnit _workUnit;

        public AdminStaffPortalController(ILogger<AdminStaffPortalController> logger, UserManager<ApplicationUser> userManager, AdminStaffPortalWorkUnit workUnit)
        {
            _logger = logger;
            _userManager = userManager;
            _workUnit = workUnit;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageAssignments()
        {
            IEnumerable<Assignment> assignments = _workUnit.AssignmentRepository.Get();

            return View(assignments);
        }

        [HttpGet]
        public IActionResult CreateAssignment()
        {
            CreateAssignmentViewModel createAssignmentViewModel = new CreateAssignmentViewModel()
            {
                Sites = _workUnit.GetSiteSelectList()
            };

            return View(createAssignmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAssignment(CreateAssignmentViewModel createAssignmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createAssignmentViewModel);
            }
            else
            {
                Assignment newAssignment = new Assignment()
                {
                    Site = _workUnit.SiteRepository.GetByID(createAssignmentViewModel.SelectedSiteID),
                    Name = createAssignmentViewModel.Name,
                    Deadline = createAssignmentViewModel.Deadline
                };
                _workUnit.AssignmentRepository.Insert(newAssignment);
                _workUnit.Save();
                return RedirectToAction("ViewAssignment", new { id = newAssignment.ID });
            }
        }

        public IActionResult ViewAssignment(int id)
        {
            Assignment assignment = _workUnit.AssignmentRepository.GetByID(id);

            if (assignment == null)
            {
                return RedirectToAction("Index");
            }

            ViewAssignmentViewModel viewAssignmentViewModel = new ViewAssignmentViewModel()
            {
                Assignment = assignment,
                Notes = _workUnit.AssignmentRepository.GetAllNotes(assignment)
            };

            return View(viewAssignmentViewModel);
        }

        public IActionResult ManageJobs()
        {
            IEnumerable<Job> jobList = _workUnit.JobRepository.Get();

            return View(jobList);
        }

        [HttpGet]
        public IActionResult CreateJob(int? assignmentId)
        {
            CreateJobViewModel createJobViewModel = new CreateJobViewModel()
            {
                Assignments = _workUnit.AssignmentRepository.GetAssignmentSelectList(),
                Products = _workUnit.ProductRepository.GetProductSelectList()
            };

            if (assignmentId != null)
            {
                createJobViewModel.SelectedAssignmentID = assignmentId.Value;
            }

            return View(createJobViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateJob(CreateJobViewModel createJobViewModel)
        {
            if (!ModelState.IsValid)
            {
                createJobViewModel.Assignments = _workUnit.AssignmentRepository.GetAssignmentSelectList();
                createJobViewModel.Products = _workUnit.ProductRepository.GetProductSelectList();
                return View(createJobViewModel);
            }
            else
            {
                // Parse component list JSON
                List<ItemProductViewModel> parsedProductList;
                List<JobProduct> jobProductList = new List<JobProduct>();

                try
                {
                    parsedProductList = JsonConvert.DeserializeObject<List<ItemProductViewModel>>(createJobViewModel.ComponentListJSON);
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

                    jobProductList.Add(new JobProduct()
                    {
                        Product = product,
                        NetPrice = itemProductViewModel.NetPrice,
                        VAT = itemProductViewModel.VAT,
                        Quantity = itemProductViewModel.Quantity
                    });
                }

                Job job = _workUnit.JobRepository.CreateJob(
                    _workUnit.AssignmentRepository.GetByID(createJobViewModel.SelectedAssignmentID),
                    createJobViewModel.DueWhen,
                    jobProductList,
                    _userManager.GetUserAsync(User).Result
                    );

                if (job == null)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("ViewJob", new { id = job.ID });
            }
        }

        public IActionResult ViewJob(int id)
        {
            Job job = _workUnit.JobRepository.GetByID(id);

            if (job == null)
            {
                return RedirectToAction("Index");
            }

            return View(job);
        }

        [HttpGet]
        public IActionResult RecallJob(int id)
        {
            Job job = _workUnit.JobRepository.GetByID(id);

            if (job == null)
            {
                return RedirectToAction("Index");
            }

            RecallJobViewModel recallJobViewModel = new RecallJobViewModel()
            {
                JobID = job.ID
            };

            return View(recallJobViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecallJob(RecallJobViewModel recallJobViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(recallJobViewModel);
            }
            else
            {
                Job job = _workUnit.JobRepository.GetByID(recallJobViewModel.JobID);
                ApplicationUser user = _userManager.GetUserAsync(User).Result;

                if (job == null || user == null)
                {
                    return RedirectToAction("Index");
                }

                if (_workUnit.JobRepository.RecallJob(job, recallJobViewModel.RecallReason, user))
                {
                    return RedirectToAction("ViewJob", new { id = recallJobViewModel.JobID });
                }

                return RedirectToAction("Index");
            }
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

        public IActionResult ManageQuotes()
        {
            IEnumerable<Quote> quotes = _workUnit.QuoteRepository.Get();

            return View(quotes);
        }

        [HttpGet]
        public IActionResult CreateQuote(int? assignmentId)
        {
            CreateQuoteViewModel createJobViewModel = new CreateQuoteViewModel()
            {
                Assignments = _workUnit.AssignmentRepository.GetAssignmentSelectList(),
                Products = _workUnit.ProductRepository.GetProductSelectList(),
                QuoteTypes = _workUnit.QuoteTypeRepository.GetQuoteTypeSelectList()
            };

            if (assignmentId != null)
            {
                createJobViewModel.SelectedAssignmentID = assignmentId.Value;
            }

            return View(createJobViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateQuote(CreateQuoteViewModel createQuoteViewModel)
        {
            if (!ModelState.IsValid)
            {
                createQuoteViewModel.Assignments = _workUnit.AssignmentRepository.GetAssignmentSelectList();
                createQuoteViewModel.Products = _workUnit.ProductRepository.GetProductSelectList();
                createQuoteViewModel.QuoteTypes = _workUnit.QuoteTypeRepository.GetQuoteTypeSelectList();
                return View(createQuoteViewModel);
            }
            else
            {
                // Parse component list JSON
                List<ItemProductViewModel> parsedProductList;
                List<QuoteProduct> quoteProductList = new List<QuoteProduct>();

                try
                {
                    parsedProductList = JsonConvert.DeserializeObject<List<ItemProductViewModel>>(createQuoteViewModel.ComponentListJSON);
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
                
                Quote quote = _workUnit.QuoteRepository.CreateQuote(
                    _workUnit.AssignmentRepository.GetByID(createQuoteViewModel.SelectedAssignmentID),
                    _workUnit.QuoteTypeRepository.GetByID(createQuoteViewModel.SelectedQuoteTypeID),
                    createQuoteViewModel.DueWhen,
                    quoteProductList,
                    _userManager.GetUserAsync(User).Result
                    );

                if (quote == null)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("ViewQuote", new { id = quote.ID });
            }
        }

        public IActionResult ViewQuote(int id)
        {
            Quote quote = _workUnit.QuoteRepository.GetByID(id);

            if (quote == null)
            {
                return RedirectToAction("Index");
            }

            return View(quote);
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

        public IActionResult ManageClients()
        {
            IEnumerable<Client> clients = _workUnit.ClientRepository.Get();
            return View(clients);
        }

        [HttpGet]
        public IActionResult CreateClient()
        {
            return View(new Client());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateClient(Client newClient)
        {
            if (!ModelState.IsValid)
            {
                return View(newClient);
            }
            else
            {
                _workUnit.ClientRepository.Insert(newClient);
                _workUnit.Save();
                return RedirectToAction("ViewClient", new { id = newClient.ID });
            }
        }

        public IActionResult ViewClient(int id)
        {
            Client client = _workUnit.ClientRepository.GetByID(id);

            if (client == null)
            {
                return RedirectToAction("Index");
            }

            IEnumerable<Assignment> assignments = _workUnit.AssignmentRepository.Get(a => client.Sites.Contains(a.Site));

            ViewClientViewModel viewClientViewModel = new ViewClientViewModel()
            {
                Client = client,
                Assignments = assignments,
                Jobs = _workUnit.JobRepository.Get(j => assignments.Contains(j.Assignment)),
                Quotes = _workUnit.QuoteRepository.Get(q => assignments.Contains(q.Assignment)),
                Complaints = _workUnit.ComplaintRepository.Get(c => assignments.Contains(c.Assignment))
            };

            return View(viewClientViewModel);
        }

        [HttpGet]
        public IActionResult EditClient(int id)
        {
            Client client = _workUnit.ClientRepository.GetByID(id);

            if (client == null)
            {
                return RedirectToAction("Index");
            }

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return View(client);
            }
            else
            {
                Client client1 = _workUnit.ClientRepository.GetByID(client.ID);
                if (client1 == null)
                {
                    return RedirectToAction("ManageClients");
                }

                client1.Name = client.Name;
                client1.ContactName = client.ContactName;
                client1.ContactEmail = client.ContactEmail;
                client1.ContactPhoneNumber = client.ContactPhoneNumber;
                client1.AddressLine1 = client.AddressLine1;
                client1.Postcode = client.Postcode;

                _workUnit.ClientRepository.Update(client1);
                _workUnit.Save();

                return RedirectToAction("ViewClient", new { id = client.ID });
            }
        }

        public IActionResult ManageIndependentSites()
        {
            IEnumerable<Site> sites = _workUnit.SiteRepository.Get(s => s.Client == null);
            return View(sites);
        }

        [HttpGet]
        public IActionResult CreateSite(int? clientId)
        {
            CreateSiteViewModel createSiteViewModel = new CreateSiteViewModel()
            {
                Clients = _workUnit.ClientRepository.GetClientSelectList().Prepend(new SelectListItem("No Client", "-1"))
            };

            if (clientId != null)
            {
                createSiteViewModel.SelectedClientID = clientId.Value;
            }

            return View(createSiteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSite(CreateSiteViewModel newSiteViewModel)
        {
            if (!ModelState.IsValid)
            {
                newSiteViewModel.Clients = _workUnit.ClientRepository.GetClientSelectList().Prepend(new SelectListItem("No Client", "-1"));
                return View(newSiteViewModel);
            }
            else
            {
                Site newSite = new Site()
                {
                    Client = _workUnit.ClientRepository.GetByID(newSiteViewModel.SelectedClientID),
                    ContactName = newSiteViewModel.ContactName,
                    ContactEmail = newSiteViewModel.ContactEmail,
                    ContactPhoneNumber = newSiteViewModel.ContactPhoneNumber,
                    AddressLine1 = newSiteViewModel.AddressLine1,
                    Postcode = newSiteViewModel.Postcode
                };
                _workUnit.SiteRepository.Insert(newSite);
                _workUnit.Save();
                return RedirectToAction("ViewSite", new { id = newSite.ID });
            }
        }

        public IActionResult ViewSite(int id)
        {
            Site site = _workUnit.SiteRepository.GetByID(id);

            if (site == null)
            {
                return RedirectToAction("Index");
            }

            IEnumerable<Assignment> assignments = _workUnit.AssignmentRepository.Get(a => a.Site == site);

            ViewSiteViewModel viewClientViewModel = new ViewSiteViewModel()
            {
                Site = site,
                Assignments = assignments,
                Jobs = _workUnit.JobRepository.Get(j => assignments.Contains(j.Assignment)),
                Quotes = _workUnit.QuoteRepository.Get(q => assignments.Contains(q.Assignment)),
                Complaints = _workUnit.ComplaintRepository.Get(c => assignments.Contains(c.Assignment))
            };

            return View(viewClientViewModel);

            return View(site);
        }

        [HttpGet]
        public IActionResult EditSite(int id)
        {
            Site site = _workUnit.SiteRepository.GetByID(id);

            if (site == null)
            {
                return RedirectToAction("Index");
            }

            EditSiteViewModel editSiteViewModel = new EditSiteViewModel()
            {
                Clients = _workUnit.ClientRepository.GetClientSelectList().Prepend(new SelectListItem("No Client", "-1")),
                ID = site.ID,
                SelectedClientID = site.Client == null ? -1 : site.Client.ID,
                ContactName = site.ContactName,
                ContactPhoneNumber = site.ContactPhoneNumber,
                ContactEmail = site.ContactEmail,
                AddressLine1 = site.AddressLine1,
                Postcode = site.Postcode
            };

            return View(editSiteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSite(EditSiteViewModel editSiteViewModel)
        {
            if (!ModelState.IsValid)
            {
                editSiteViewModel.Clients = _workUnit.ClientRepository.GetClientSelectList().Prepend(new SelectListItem("No Client", "-1"));
                return View(editSiteViewModel);
            }
            else
            {
                Site site = _workUnit.SiteRepository.GetByID(editSiteViewModel.ID);

                if (site == null)
                {
                    return RedirectToAction("Index");
                }

                if (site.Client != null)
                {
                    site.Client.Sites.Remove(site);
                }

                site.Client = _workUnit.ClientRepository.GetByID(editSiteViewModel.SelectedClientID);
                site.ContactName = editSiteViewModel.ContactName;
                site.ContactPhoneNumber = editSiteViewModel.ContactPhoneNumber;
                site.ContactEmail = editSiteViewModel.ContactEmail;
                site.AddressLine1 = editSiteViewModel.AddressLine1;
                site.Postcode = editSiteViewModel.Postcode;

                _workUnit.SiteRepository.Update(site);
                _workUnit.Save();

                return RedirectToAction("ViewSite", new { id = site.ID });
            }
        }

        public IActionResult ManageComplaints()
        {
            IEnumerable<Complaint> complaints = _workUnit.ComplaintRepository.Get();

            return View(complaints);
        }
    }
}
