using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NestLinkV2.Models;

namespace NestLinkV2.Areas.ClientPortal.ViewModels
{
    public class ClientDashboardViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Job> ActiveJobs { get; set; }
        public List<Quote> PendingQuotes { get; set; }
    }
}
