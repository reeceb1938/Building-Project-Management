using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [ForeignKey("ProductTypeId")]
        [Display(Name = "Product Type")]
        public virtual ProductType ProductType { get; set; }
        [Required]
        [Display(Name = "Net Price")]
        [Column(TypeName = "Decimal(18, 2)")]
        public decimal NetPrice { get; set; }
        [Required]
        [Display(Name = "VAT")]
        [Column(TypeName = "Decimal(18, 2)")]
        public decimal VAT { get; set; }

        public virtual List<JobProduct> JobProducts { get; set; }

        [NotMapped]
        public decimal GrossPrice
        {
            get
            {
                return NetPrice + VAT;
            }
        }
    }
}
