using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class JobNote : Note
    {
        [Required]
        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }
    }
}
