using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.ViewModels
{
    public class ItemProductViewModel
    {
        [Required]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }
        [Required]
        [Display(Name = "Net Price")]
        public decimal NetPrice { get; set; }
        [Required]
        [Display(Name = "VAT")]
        public decimal VAT { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}
