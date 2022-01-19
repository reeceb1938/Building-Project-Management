using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.TechnicianPortal.ViewModels
{
    public class ViewJobViewModel
    {
        public ApplicationUser User { get; set; }
        public Job Job { get; set; } 
    }
}
