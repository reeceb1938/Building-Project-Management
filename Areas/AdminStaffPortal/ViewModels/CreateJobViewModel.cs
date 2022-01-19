
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.AdminStaffPortal.ViewModels
{
    public class CreateJobViewModel
    {
        [Required]
        [Display(Name = "Assignment")]
        public int SelectedAssignmentID { get; set; }
        public IEnumerable<SelectListItem> Assignments { get; set; }
        [Required]
        [Display(Name = "Product")]
        public int SelectedProductID { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
        [Required]
        [Display(Name = "Deadline")]
        public DateTime DueWhen { get; set; }

        [Required]
        [Display(Name = "Component List JSON")]
        public string ComponentListJSON { get; set; }
    }
}
