using Microsoft.AspNetCore.Mvc.Rendering;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.Settings.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Product Type")]
        public int SelectedProductTypeID { get; set; }
        public IEnumerable<SelectListItem> ProductTypes { get; set; }
        [Required]
        [Display(Name = "Net Price")]
        public decimal NetPrice { get; set; }
        [Required]
        [Display(Name = "VAT")]
        public decimal VAT { get; set; }
    }
}
