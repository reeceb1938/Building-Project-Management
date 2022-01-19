using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.ViewModels
{
    public class JobAddNoteViewModel
    {
        [Required]
        [Display(Name = "Job ID")]
        public int JobID { get; set; }
        [Required]
        [Display(Name = "Note")]
        public string Text { get; set; }
    }
}
