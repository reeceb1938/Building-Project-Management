using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [ForeignKey("SiteId")]
        [Display(Name = "Site")]
        public virtual Site Site { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }

        public virtual List<Quote> Quotes { get; set; }
        public virtual List<Job> Jobs { get; set; }
        public virtual List<Complaint> Complaints { get; set; }
        public virtual List<WorkItem> WorkItems { get; set; }
    }
}
