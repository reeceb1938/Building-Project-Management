using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NestLinkV2.Data
{
    public interface IDbInitializer
    {
        void Initialize();
    }

    public class DbInitializer : IDbInitializer
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(IConfiguration configuration, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Initialize()
        {
            string superadminEmail = _configuration.GetValue<string>("SuperUser:Email");
            string superadminUsername = _configuration.GetValue<string>("SuperUser:Username");
            string superadminPassword = _configuration.GetValue<string>("SuperUser:Password");
            string superadminDefaultRole = _configuration.GetValue<string>("SuperUser:Role");


#nullable enable
            ApplicationUser? user = _userManager.FindByEmailAsync(superadminEmail).Result;
#nullable disable

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = superadminUsername,
                    Email = superadminEmail,
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(user, superadminPassword);
            }

            string[] roles = new string[] { superadminDefaultRole, "Technician", "Client", "AdminStaff" };

            foreach (string role in roles)
            {
                if (!_context.Roles.Any(r => r.Name == role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
