using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class Complaint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [ForeignKey("AssignmentId")]
        [Display(Name = "Assignment")]
        public virtual Assignment Assignment { get; set; }

        public virtual List<ComplaintEventHistory> EventHistory { get; set; }
    }
}
