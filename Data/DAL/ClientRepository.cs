using Microsoft.AspNetCore.Mvc.Rendering;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Data.DAL
{
    public class ClientRepository : GenericRepository<Client>
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<SelectListItem> GetClientSelectList()
        {
            IEnumerable<SelectListItem> results = context.Clients.Select(c => new SelectListItem()
            {
                Value = c.ID.ToString(),
                Text = c.Name
            });

            return results;
        }
    }
}
