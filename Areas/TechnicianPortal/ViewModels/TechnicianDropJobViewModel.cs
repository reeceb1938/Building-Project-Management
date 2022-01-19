using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.TechnicianPortal.ViewModels
{
    public class TechnicianDropJobViewModel
    {
        [Required]
        public int JobID { get; set; }
        [Required]
        public string Reason { get; set; }
    }
}
