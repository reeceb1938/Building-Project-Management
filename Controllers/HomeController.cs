using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NestLinkV2.Models;
using NestLinkV2.ViewModels;
using NestLinkV2.Services;
using NestLinkV2.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NestLinkV2.Library.Mail;

namespace NestLinkV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailService _mailService;
        private readonly IFileTemplateService _fileTemplateService;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, IMailService mailService, IFileTemplateService fileTemplateService, ILogger<HomeController> logger)
        {
            _context = context;
            _mailService = mailService;
            _logger = logger;
            _fileTemplateService = fileTemplateService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendExampleEmailButton()
        {
            SendExampleEmail("john", DateTime.Now.ToString(), "Leaking Tap", "£20", "Fix", "NC1001", "nwl");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SendExampleEmail(string FirstName, string quoteTime, string problem, string totalCost, string solution, string refnumb, string url)
        {
            string body = _fileTemplateService.getExampleEmailBody(FirstName, quoteTime, problem, totalCost, solution, refnumb, url);
            Mail mail = new Mail
            {
                ToEmail = "arran.jones@clickfix.co",
                Subject = "This is an Example email",
                Body = body,
                Attachments = null
            };
            _mailService.SendEmailNow(mail);
        }
    }
}
