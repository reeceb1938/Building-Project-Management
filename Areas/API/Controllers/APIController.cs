using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NestLinkV2.Areas.API.Data.DAL;
using NestLinkV2.Areas.API.ViewModels;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.API.Controllers
{
    [Area("API")]
    [Produces("application/json")]
    [Authorize(Roles = "AdminStaff")]
    public class APIController : Controller
    {
        private readonly ILogger<APIController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly APIWorkUnit _workUnit;

        public APIController(ILogger<APIController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, APIWorkUnit workUnit)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _workUnit = workUnit;
        }

        public IActionResult GetItemProductInformation(int id)
        {
            Product product = _workUnit.ProductRepository.GetByID(id);

            if (product == null)
            {
                return NotFound();
            }

            QuoteProductInformationViewModel quoteProductInformationViewModel = new QuoteProductInformationViewModel()
            {
                ProductID = product.ID,
                Name = product.Name,
                Description = product.Description,
                ProductTypeName = product.ProductType.Name,
                NetPrice = product.NetPrice,
                VAT = product.VAT
            };

            return Json(quoteProductInformationViewModel);
        }
    }
}
