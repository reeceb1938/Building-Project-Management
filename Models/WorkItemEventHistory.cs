using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class WorkItemEventHistory : EventHistory
    {
        [Required]
        [ForeignKey("WorkItemId")]
        public virtual WorkItem WorkItem { get; set; }
    }
}
