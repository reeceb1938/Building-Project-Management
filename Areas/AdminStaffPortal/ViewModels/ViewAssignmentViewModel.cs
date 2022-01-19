using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.AdminStaffPortal.ViewModels
{
    public class ViewAssignmentViewModel
    {
        public Assignment Assignment { get; set; }
        public IEnumerable<Note> Notes { get; set; }
    }
}
