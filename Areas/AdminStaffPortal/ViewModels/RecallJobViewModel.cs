using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.AdminStaffPortal.ViewModels
{
    public class RecallJobViewModel
    {
        [Required]
        [Display(Name = "Job ID")]
        public int JobID { get; set; }
        [Required]
        [Display(Name = "Recall Reason")]
        public string RecallReason { get; set; }
    }
}
