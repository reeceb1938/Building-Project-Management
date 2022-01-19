using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NestLinkV2.Models;
using NestLinkV2.Services;
using NestLinkV2.Data;
using NestLinkV2.Data.DAL;
using NestLinkV2.Library.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotativa.AspNetCore;

namespace NestLinkV2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Stripe.StripeConfiguration.ApiKey = Configuration.GetSection("Stripe").GetSection("APIKey").Value;
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(
                        Configuration.GetConnectionString("DatabaseConnection")));
            services.AddWorkUnits();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Lockout.AllowedForNewUsers = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DatabaseConnection")));
            services.AddHangfireServer();

            services.AddControllersWithViews()
                .AddNewtonsoftJson();
            services.AddRazorPages();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IFileTemplateService, FileTemplateService>();



            services.AddScoped<IDbInitializer, DbInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext, IDbInitializer dbInitializer)
        {
            dbContext.Database.Migrate();
            dbInitializer.Initialize();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard();
            /*app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });*/

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "TechnicianPortal",
                    areaName: "TechnicianPortal",
                    pattern: "TechnicianPortal/{action=Index}/{id?}",
                    defaults: new { controller = "TechnicianPortal", Area = "TechnicianPortal" });
                endpoints.MapAreaControllerRoute(
                    name: "ClientPortal",
                    areaName: "ClientPortal",
                    pattern: "ClientPortal/{action=Index}/{id?}",
                    defaults: new { controller = "ClientPortal", Area = "ClientPortal" });
                endpoints.MapAreaControllerRoute(
                    name: "AdminStaffPortal",
                    areaName: "AdminStaffPortal",
                    pattern: "AdminStaffPortal/{action=Index}/{id?}",
                    defaults: new { controller = "AdminStaffPortal", Area = "AdminStaffPortal" });
                endpoints.MapAreaControllerRoute(
                    name: "Settings",
                    areaName: "Settings",
                    pattern: "Settings/{action=Index}/{id?}",
                    defaults: new { controller = "Settings", area = "Settings" });
                endpoints.MapAreaControllerRoute(
                    name: "API",
                    areaName: "API",
                    pattern: "API/{action=Index}/{id?}",
                    defaults: new { controller = "API", area = "API" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            RotativaConfiguration.Setup(env.WebRootPath, "Rotativa");
        }
    }
}
