using Microsoft.AspNetCore.Mvc.Rendering;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Data.DAL
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Product CreateProduct(string name, string description, ProductType productType, Decimal netPrice, Decimal vat)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || productType == null)
            {
                return null;
            }

            Product product = new Product()
            {
                Name = name,
                Description = description,
                ProductType = productType,
                NetPrice = netPrice,
                VAT = vat
            };
            context.Products.Add(product);
            context.SaveChanges();

            return product;
        }

        public IEnumerable<SelectListItem> GetProductSelectList()
        {
            IEnumerable<SelectListItem> results = context.Products.Select(a => new SelectListItem()
            {
                Value = a.ID.ToString(),
                Text = a.Name
            });

            return results;
        }
    }
}
