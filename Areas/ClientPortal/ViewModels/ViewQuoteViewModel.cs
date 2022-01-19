using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.ClientPortal.ViewModels
{
    public class ViewQuoteViewModel
    {
        public ApplicationUser User { get; set; }
        public Quote Quote { get; set; }
    }
}
