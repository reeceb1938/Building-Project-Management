using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class WorkItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [ForeignKey("AssignmentId")]
        [Display(Name = "Assignment")]
        public virtual Assignment Assignment { get; set; }
        // NOTE: This must not be [Required] to avoid CASCADE DELETE / UPDATE loops
        [ForeignKey("AspNetUserId")]
        [Display(Name = "Assigned To")]
        public virtual ApplicationUser AssignedTo { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Details")]
        public string Details { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DueWhen { get; set; }

        public virtual List<WorkItemEventHistory> EventHistory { get; set; }
    }
}
