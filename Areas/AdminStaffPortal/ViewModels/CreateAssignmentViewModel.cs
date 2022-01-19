using Microsoft.AspNetCore.Mvc.Rendering;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.AdminStaffPortal.ViewModels
{
    public class CreateAssignmentViewModel
    {
        [Required]
        [Display(Name = "Site")]
        public int SelectedSiteID { get; set; }
        public IEnumerable<SelectListItem> Sites { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }
    }
}
