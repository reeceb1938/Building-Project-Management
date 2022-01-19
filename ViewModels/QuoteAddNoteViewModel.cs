using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.ViewModels
{
    public class QuoteAddNoteViewModel
    {
        [Required]
        [Display(Name = "Quote ID")]
        public int QuoteID { get; set; }
        [Required]
        [Display(Name = "Note")]
        public string Text { get; set; }
    }
}
