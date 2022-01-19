using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class QuoteNote : Note
    {
        [Required]
        [ForeignKey("QuoteId")]
        public virtual Quote Quote { get; set; }
    }
}
