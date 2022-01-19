using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NestLinkV2.Areas.ClientPortal.Data;
using NestLinkV2.Areas.ClientPortal.ViewModels;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.ClientPortal.Controllers
{
    [Area("ClientPortal")]
    [Authorize(Roles = "Client")]
    public class ClientPortalController : Controller
    {
        private readonly ILogger<ClientPortalController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private ClientPortalWorkUnit _workUnit;

        public ClientPortalController(ILogger<ClientPortalController> logger, UserManager<ApplicationUser> userManager, ClientPortalWorkUnit workUnit)
        {
            _logger = logger;
            _userManager = userManager;
            _workUnit = workUnit;
        }

        public IActionResult Index()
        {
            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            if (user == null)
            {
                return Unauthorized();
            }

            if (user.ClientUser == null)
            {
                return RedirectToAction("ClientAccountBadConfig");
            }

            List<Job> j1 = _workUnit.GetClientActiveJobs(user.ClientUser.Client).ToList();
            List<Quote> q1 = _workUnit.GetClientPendingQuotes(user.ClientUser.Client).ToList();

            ClientDashboardViewModel clientDashboardViewModel = new ClientDashboardViewModel()
            {
                User = user,
                ActiveJobs = _workUnit.GetClientActiveJobs(user.ClientUser.Client).ToList(),
                PendingQuotes = _workUnit.GetClientPendingQuotes(user.ClientUser.Client).ToList()
            };

            return View(clientDashboardViewModel);
        }

        public IActionResult ClientAccountBadConfig()
        {
            return View();
        }

        public IActionResult ViewQuote(int id)
        {
            Quote quote = _workUnit.QuoteRepository.GetByID(id);
            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            if (quote == null || user == null || user.ClientUser == null)
            {
                return RedirectToAction("Index");
            }

            ViewQuoteViewModel quoteViewModel = new ViewQuoteViewModel()
            {
                User = user,
                Quote = quote
            };

            return View(quoteViewModel);
        }

        public IActionResult ApproveQuote(int id)
        {
            Quote quote = _workUnit.QuoteRepository.GetByID(id);
            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            if (quote == null || user == null || user.ClientUser == null)
            {
                return RedirectToAction("Index");
            }

            if (_workUnit.QuoteRepository.ClientApproveQuote(quote, user))
            {
                return RedirectToAction("ViewQuote", new { id = quote.ID });
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeclineQuote(int id)
        {
            Quote quote = _workUnit.QuoteRepository.GetByID(id);
            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            if (quote == null || user == null || user.ClientUser == null)
            {
                return RedirectToAction("Index");
            }

            if (_workUnit.QuoteRepository.ClientDeclineQuote(quote, user))
            {
                return RedirectToAction("ViewQuote", new { id = quote.ID });
            }

            return RedirectToAction("Index");
        }
    }
}
