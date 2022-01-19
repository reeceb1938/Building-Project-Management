using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Data.DAL
{
    public class QuoteProductRepository : GenericRepository<QuoteProduct>
    {
        public QuoteProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
