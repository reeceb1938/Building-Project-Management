using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.TechnicianPortal.ViewModels
{
    public class TechnicianPickupJobViewModel
    {
        [Required]
        [Display(Name = "Job ID")]
        public int JobID { get; set; }
        [Required]
        [Display(Name = "ETA")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ETA { get; set; }
    }
}
