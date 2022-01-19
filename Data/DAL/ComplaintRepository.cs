using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Data.DAL
{
    public class ComplaintRepository : GenericRepository<Complaint>
    {
        public ComplaintRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
