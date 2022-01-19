using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.AdminStaffPortal.ViewModels
{
    public class ViewSiteViewModel
    {
        public Site Site { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<Quote> Quotes { get; set; }
        public IEnumerable<WorkItem> WorkItems { get; set; }
        public IEnumerable<Complaint> Complaints { get; set; }
    }
}
