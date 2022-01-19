using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.TechnicianPortal.ViewModels
{
    public class TechnicianScheduleQuoteSurveyViewModel
    {
        [Required]
        public int QuoteID { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Appointment Date & Time")]
        public DateTime AppointmentDateTime { get; set; }
    }
}
