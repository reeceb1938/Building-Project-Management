using Microsoft.Extensions.DependencyInjection;
using NestLinkV2.Areas.AdminStaffPortal.Data.DAL;
using NestLinkV2.Areas.API.Data.DAL;
using NestLinkV2.Areas.ClientPortal.Data;
using NestLinkV2.Areas.Settings.Data.DAL;
using NestLinkV2.Areas.TechnicianPortal.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Data.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorkUnits(this IServiceCollection services)
        {
            services.AddTransient<SettingsWorkUnit>();
            services.AddTransient<TechnicianPortalWorkUnit>();
            services.AddTransient<ClientPortalWorkUnit>();
            services.AddTransient<AdminStaffPortalWorkUnit>();
            services.AddTransient<APIWorkUnit>();

            return services;
        }
    }
}
