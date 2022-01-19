using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class QuoteAppointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [ForeignKey("QuoteId")]
        [Display(Name = "Quote")]
        public virtual Quote Quote { get; set; }
        [Required]
        [ForeignKey("TechnicianId")]
        [Display(Name = "Technician")]
        public virtual Technician Technician { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "When")]
        public DateTime When { get; set; }
    }
}
