using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.TechnicianPortal.ViewModels
{
    public class TechnicianUpdateJobETAViewModel
    {
        [Required]
        [Display(Name = "ID")]
        public int JobID { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "New ETA")]
        public DateTime ETA { get; set; }
    }
}
