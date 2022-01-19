using Microsoft.AspNetCore.Mvc.Rendering;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Data.DAL
{
    public class QuoteTypeRepository : GenericRepository<QuoteType>
    {
        public QuoteTypeRepository(ApplicationDbContext context) : base(context)
        { 
        }

        public IEnumerable<SelectListItem> GetQuoteTypeSelectList()
        {
            IEnumerable<SelectListItem> results = context.QuoteTypes.Select(a => new SelectListItem()
            {
                Value = a.ID.ToString(),
                Text = a.Name
            });

            return results;
        }
    }
}
