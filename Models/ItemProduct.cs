using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class ItemProduct
    {
        [Required]
        [Display(Name = "Product")]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [Required]
        [Display(Name = "Net Price")]
        [Column(TypeName = "Decimal(18, 2)")]
        public decimal NetPrice { get; set; }
        [Required]
        [Display(Name = "VAT")]
        [Column(TypeName = "Decimal(18, 2)")]
        public decimal VAT { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [NotMapped]
        public decimal GrossPricePerUnit
        {
            get
            {
                return NetPrice + VAT;
            }
        }

        [NotMapped]
        public decimal GrossPrice
        {
            get
            {
                return GrossPricePerUnit * Quantity;
            }
        }
    }
}
