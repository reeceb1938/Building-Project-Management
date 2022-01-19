using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class ClientUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [ForeignKey("AspNetUserId")]
        [Display(Name = "User Account")]
        public virtual ApplicationUser User { get; set; }
        [Required]
        [ForeignKey("ClientId")]
        [Display(Name = "Client")]
        public virtual Client Client { get; set; }
    }
}
