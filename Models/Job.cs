using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class Job
    {
        public Job()
        {
            EventHistory = new List<JobEventHistory>();
            JobNotes = new List<JobNote>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [ForeignKey("QuoteId")]
        [Display(Name = "Quote")]
        public virtual Quote Quote { get; set; }
        [Required]
        [ForeignKey("AssignmentId")]
        [Display(Name = "Assignment")]
        public virtual Assignment Assignment { get; set; }
        [Required]
        [ForeignKey("JobStatusId")]
        [Display(Name = "Status")]
        public virtual JobStatus JobStatus { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deadline")]
        public DateTime DueWhen { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "ETA")]
        public DateTime ETA { get; set; }

        public virtual List<JobEventHistory> EventHistory { get; set; }
#nullable enable
        public virtual Technician? CurrentTechnician { get; set; }
#nullable disable
        public virtual List<JobProduct> JobProducts { get; set; }
        public virtual List<JobNote> JobNotes { get; set; }

        [NotMapped]
        public Product PrimaryProduct
        {
            get
            {
                return JobProducts.Where(jp => jp.Product.ProductType.ID == (int)Enums.ProductTypes.Product).First().Product;
            }
        }

        [NotMapped]
        public decimal GrossCost
        {
            get
            {
                return JobProducts.Sum(jp => jp.GrossPrice);
            }
        }

        [NotMapped]
        public decimal NetCost
        {
            get
            {
                return JobProducts.Sum(jp => jp.NetPrice);
            }
        }

        [NotMapped]
        public decimal TotalVAT
        {
            get
            {
                return JobProducts.Sum(jp => jp.VAT);
            }
        }
    }
}
