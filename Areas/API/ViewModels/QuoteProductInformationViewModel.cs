using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.API.ViewModels
{
    public class QuoteProductInformationViewModel
    {
        [Required]
        [Display(Name = "ProductID")]
        public int ProductID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Product Type Name")]
        public string ProductTypeName { get; set; }
        [Required]
        [Display(Name = "Net Price")]
        public decimal NetPrice { get; set; }
        [Required]
        [Display(Name = "VAT")]
        public decimal VAT { get; set; }
    }
}
