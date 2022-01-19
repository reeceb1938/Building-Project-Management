using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    [NotMapped]
    public class EventHistory
    {
        public EventHistory()
        {
            WhenCreated = DateTime.Now;
            Reason = string.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [ForeignKey("EventTypeId")]
        [Display(Name = "Event Type")]
        public virtual EventType EventType{ get; set; }
        [Required]
        [ForeignKey("AspNetUserId")]
        [Display(Name = "User Performed By")]
        public virtual ApplicationUser User { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created When")]
        public DateTime WhenCreated { get; set; }
        [Display(Name = "Reason")]
        public string Reason { get; set; }
    }
}
