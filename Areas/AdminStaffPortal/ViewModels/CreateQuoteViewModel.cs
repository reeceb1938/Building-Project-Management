using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.AdminStaffPortal.ViewModels
{
    public class CreateQuoteViewModel
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
        [Display(Name = "Quote Type")]
        public int SelectedQuoteTypeID { get; set; }
        public IEnumerable<SelectListItem> QuoteTypes { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deadline")]
        public DateTime DueWhen { get; set; }
        [Required]
        [Display(Name = "Component List JSON")]
        public string ComponentListJSON { get; set; }
    }
}
