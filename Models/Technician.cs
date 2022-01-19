using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class Technician
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [ForeignKey("AspNetUserId")]
        [Display(Name = "User Account")]
        public virtual ApplicationUser User { get; set; }
#nullable enable
        [ForeignKey("CurrentJobId")]
        [Display(Name = "Current Job")]
        public virtual Job? CurrentJob { get; set; }
#nullable disable

        public virtual List<QuoteAppointment> QuoteAppointments { get; set; }
    }
}
