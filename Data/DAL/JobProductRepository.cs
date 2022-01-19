using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Data.DAL
{
    public class JobProductRepository : GenericRepository<JobProduct>
    {
        public JobProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
