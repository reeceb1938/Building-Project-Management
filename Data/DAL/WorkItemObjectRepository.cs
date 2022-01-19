using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Data.DAL
{
    public class WorkItemObjectRepository : GenericRepository<WorkItemObject>
    {
        public WorkItemObjectRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
