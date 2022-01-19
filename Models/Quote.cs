using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class Quote
    {
        public Quote()
        {
            EventHistory = new List<QuoteEventHistory>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [ForeignKey("AssignmentId")]
        [Display(Name = "Assignment")]
        public virtual Assignment Assignment { get; set; }
        [Required]
        [ForeignKey("QuoteTypeId")]
        [Display(Name = "Quote Type")]
        public virtual QuoteType QuoteType { get; set; }
        [Required]
        [ForeignKey("QuoteStatusId")]
        [Display(Name = "Quote Status")]
        public virtual QuoteStatus QuoteStatus { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deadline")]
        public DateTime DueWhen { get; set; }

        public virtual List<QuoteEventHistory> EventHistory { get; set; }
#nullable enable
        public virtual QuoteAppointment? QuoteAppointment { get; set; }
#nullable disable
        public virtual List<QuoteProduct> QuoteProducts { get; set; }
        public virtual List<QuoteNote> QuoteNotes { get; set; }

        [NotMapped]
        public Product PrimaryProduct
        {
            get
            {
                return QuoteProducts.Where(qp => qp.Product.ProductType.ID == (int)Enums.ProductTypes.Product).First().Product;
            }
        }

        [NotMapped]
        public decimal GrossCost
        {
            get
            {
                return QuoteProducts.Sum(qp => qp.GrossPrice);
            }
        }

        [NotMapped]
        public decimal NetCost
        {
            get
            {
                return QuoteProducts.Sum(qp => qp.NetPrice);
            }
        }

        [NotMapped]
        public decimal TotalVAT
        {
            get
            {
                return QuoteProducts.Sum(qp => qp.VAT);
            }
        }
    }
}
