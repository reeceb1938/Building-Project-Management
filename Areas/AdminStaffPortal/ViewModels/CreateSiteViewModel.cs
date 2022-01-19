using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.AdminStaffPortal.ViewModels
{
    public class CreateSiteViewModel
    {
        public CreateSiteViewModel()
        {
            Clients = new List<SelectListItem>();
        }

        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Client")]
        public int SelectedClientID { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }
        [Required]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Contact Number")]
        public string ContactPhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }
        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }
        [Required]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }
    }
}
