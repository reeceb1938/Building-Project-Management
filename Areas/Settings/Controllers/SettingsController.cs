using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NestLinkV2.Areas.Settings.Data.DAL;
using NestLinkV2.Areas.Settings.ViewModels;
using NestLinkV2.Data;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.Settings.Controllers
{
    [Area("Settings")]
    public class SettingsController : Controller
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly SettingsWorkUnit _workUnit;

        public SettingsController(ILogger<SettingsController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, SettingsWorkUnit workUnit)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _workUnit = workUnit;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Settings/MyCompany")]
        public IActionResult MyCompany()
        {
            return View();
        }

        [Route("Settings/MyCompany/ManageProducts")]
        public IActionResult ManageProducts()
        {
            IEnumerable<Product> products = _workUnit.ProductRepository.Get();

            return View(products);
        }

        [HttpGet]
        [Route("Settings/MyCompany/CreateProduct")]
        public IActionResult CreateProduct()
        {
            CreateProductViewModel createProductViewModel = new CreateProductViewModel()
            {
                ProductTypes = _workUnit.ProductTypeRepository.GetProductTypeSelectList()
            };

            return View(createProductViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Settings/MyCompany/CreateProduct")]
        public IActionResult CreateProduct(CreateProductViewModel createProductViewModel)
        {
            createProductViewModel.ProductTypes = _workUnit.ProductTypeRepository.GetProductTypeSelectList();

            if (!ModelState.IsValid)
            {
                return View(createProductViewModel);
            }
            else
            {
                ProductType productType = _workUnit.ProductTypeRepository.GetByID(createProductViewModel.SelectedProductTypeID);

                if (productType == null)
                {
                    ModelState.AddModelError("", "Invalid product type selected");
                    return View(createProductViewModel);
                }

                Product newProduct = _workUnit.ProductRepository.CreateProduct(createProductViewModel.Name, createProductViewModel.Description, productType, createProductViewModel.NetPrice, createProductViewModel.VAT);
                _workUnit.Save();

                if (newProduct == null)
                {
                    return RedirectToAction("ManageProducts");
                }

                return RedirectToAction("ManageProducts");
            }
        }

        [HttpGet]
        [Route("Settings/MyCompany/EditProduct/{id}")]
        public IActionResult EditProduct(int id)
        {
            Product product = _workUnit.ProductRepository.GetByID(id);

            if (product == null)
            {
                return RedirectToAction("ManageProducts");
            }

            EditProductViewModel editProductViewModel = new EditProductViewModel()
            {
                ID = product.ID,
                Name = product.Name,
                Description = product.Description,
                SelectedProductTypeID = product.ProductType.ID,
                ProductTypes = _workUnit.ProductTypeRepository.GetProductTypeSelectList(),
                NetPrice = product.NetPrice,
                VAT = product.VAT
            };

            return View(editProductViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Settings/MyCompany/EditProduct/{id}")]
        public IActionResult EditProduct(EditProductViewModel editProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editProductViewModel);
            }
            else
            {
                Product product = _workUnit.ProductRepository.GetByID(editProductViewModel.ID);

                if (product == null)
                {
                    return RedirectToAction("Index");
                }

                product.Name = editProductViewModel.Name;
                product.Description = editProductViewModel.Description;
                product.ProductType = _workUnit.ProductTypeRepository.GetByID(editProductViewModel.SelectedProductTypeID);
                product.NetPrice = editProductViewModel.NetPrice;
                product.VAT = editProductViewModel.VAT;

                _workUnit.ProductRepository.Update(product);
                _workUnit.Save();
                return RedirectToAction("ManageProducts");
            }
        }

        [Route("Settings/MyCompany/DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            Product product = _workUnit.ProductRepository.GetByID(id);

            if (product == null)
            {
                return RedirectToAction("ManageProducts");
            }

            _workUnit.ProductRepository.Delete(product.ID);
            _workUnit.Save();

            return RedirectToAction("ManageProducts");
        }

        [Route("Settings/UserAccounts")]
        public IActionResult UserAccounts()
        {
            UserAccountRolesViewModel userAccountRolesViewModel = new UserAccountRolesViewModel()
            {
                Users = _userManager.Users.ToList(),
                Roles = _roleManager.Roles.ToList()
            };

            return View(userAccountRolesViewModel);
        }

        [HttpGet]
        [Route("Settings/UserAccounts/CreateUserRole")]
        public IActionResult CreateUserRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Settings/UserAccounts/CreateUserRole")]
        public async Task<IActionResult> CreateUserRole(CreateUserRoleViewModel createUserRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createUserRoleViewModel);
            }
            else
            {
                if (_roleManager.RoleExistsAsync(createUserRoleViewModel.RoleName).Result)
                {
                    ModelState.AddModelError("", string.Format("{0} already exists", createUserRoleViewModel.RoleName));
                    return View(createUserRoleViewModel);
                }
                else
                {
                    await _roleManager.CreateAsync(new IdentityRole(createUserRoleViewModel.RoleName));
                    return RedirectToAction("UserAccounts");
                }
            }
        }

        [Route("Settings/UserAccounts/DeleteUserRole/{id}")]
        public async Task<IActionResult> DeleteUserRole(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                IdentityRole role = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(role);
            }

            return RedirectToAction("UserAccounts");
        }

        [HttpGet]
        [Route("Settings/UserAccounts/CreateUserAccount")]
        public IActionResult CreateUserAccount()
        {
            ViewBag.AvailableUserRoles = GetUserRolesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Settings/UserAccounts/CreateUserAccount")]
        public async Task<IActionResult> CreateUserAccount(CreateUserAccountViewModel createUserAccountViewModel)
        {
            ViewBag.AvailableUserRoles = GetUserRolesSelectList();
            if (!ModelState.IsValid)
            {
                return View(createUserAccountViewModel);
            }
            else
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = createUserAccountViewModel.Name,
                    Email = createUserAccountViewModel.Email,
                    EmailConfirmed = true
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, createUserAccountViewModel.Password);
                if (result.Succeeded)
                {
                    foreach (string roleId in createUserAccountViewModel.UserRoles)
                    {
                        IdentityRole role = _roleManager.FindByIdAsync(roleId).Result;
                        await _userManager.AddToRoleAsync(newUser, role.Name);
                    }

                    return RedirectToAction("EditUserAccount", new { id = newUser.Id });
                }
                else
                {
                    _logger.LogError("Account creation failed");
                    foreach (IdentityError error in result.Errors)
                    {
                        _logger.LogError("Account creation error ({0}): {1}", error.Code, error.Description);
                    }
                    ModelState.AddModelError("", "Account creation failed");
                    return View(createUserAccountViewModel);
                }
            }
            return View();
        }

        [Route("Settings/UserAccounts/EditUserAccount/{id}")]
        public IActionResult EditUserAccount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("UserAccounts");
            }

            ViewBag.AvailableUserRoles = GetUserRolesSelectList();

            ApplicationUser user = _userManager.FindByIdAsync(id).Result;
            IList<string> roles = _userManager.GetRolesAsync(user).Result;

            EditUserAccountViewModel editUserAccountViewModel = new EditUserAccountViewModel()
            {
                ID = user.Id,
                Name = user.UserName,
                Email = user.Email,
                AccountDisabled = user.LockoutEnabled,
                UserRoles = roles
            };

            return View(editUserAccountViewModel);
        }

        [HttpPost]
        [Route("Settings/UserAccounts/EditUserAccount/{id}")]
        public async Task<IActionResult> EditUserAccount(EditUserAccountViewModel editUserAccountViewModel)
        {
            ViewBag.AvailableUserRoles = GetUserRolesSelectList();
            if (!ModelState.IsValid)
            {
                return View(editUserAccountViewModel);
            }
            else
            {
                ApplicationUser user = _userManager.FindByIdAsync(editUserAccountViewModel.ID).Result;

                user.Email = editUserAccountViewModel.Email;
                user.UserName = editUserAccountViewModel.Name;
                user.LockoutEnabled = editUserAccountViewModel.AccountDisabled;

                await _userManager.UpdateAsync(user);

                IList<string> currentRoles = _userManager.GetRolesAsync(user).Result;

                foreach (string role in currentRoles)
                {
                    if (!editUserAccountViewModel.UserRoles.Contains(role))
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                    }
                }

                foreach (string role in editUserAccountViewModel.UserRoles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
                
                await _signInManager.SignInAsync(user, false, null);

                return RedirectToAction("UserAccounts");
            }
        }

        private List<SelectListItem> GetUserRolesSelectList()
        {
            List<SelectListItem> roleSelectList = new List<SelectListItem>();
            List<IdentityRole> roles = _roleManager.Roles.ToList();

            foreach (IdentityRole role in roles)
            {
                roleSelectList.Add(new SelectListItem(role.Name, role.Id));
            }

            return roleSelectList;
        }
    }
}
