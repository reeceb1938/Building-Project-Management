using Microsoft.AspNetCore.Mvc.Rendering;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.TechnicianPortal.ViewModels
{
    public class CompleteQuoteSurveyViewModel
    {
        [Required]
        [Display(Name = "Quote ID")]
        public int QuoteID { get; set; }
        [Required]
        [Display(Name = "Component List JSON")]
        public string ComponentListJSON { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }
        public IEnumerable<QuoteProduct> QuoteProducts { get; set; }
    }
}
