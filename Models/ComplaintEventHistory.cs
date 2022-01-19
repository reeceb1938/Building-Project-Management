using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class ComplaintEventHistory : EventHistory
    {
        [Required]
        [ForeignKey("ComplaintId")]
        [Display(Name = "Complaint")]
        public virtual Complaint Complaint { get; set; }
    }
}
