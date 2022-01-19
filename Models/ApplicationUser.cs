using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
        }

#nullable enable
        public virtual Technician? Technician { get; set; }
        public virtual ClientUser? ClientUser { get; set; }
#nullable disable
        public virtual List<WorkItem> WorkItems { get; set; }
    }
}
