using Microsoft.AspNetCore.Mvc.Rendering;
using NestLinkV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Data.DAL
{
    public class AssignmentRepository : GenericRepository<Assignment>
    {
        public AssignmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Note> GetAllNotes(Assignment assignment)
        {
            IEnumerable<Note> results = new List<Note>();

            results = results.Concat(context.JobNotes.Where(jn => assignment.Jobs.Contains(jn.Job)));
            results = results.Concat(context.QuoteNotes.Where(qn => assignment.Quotes.Contains(qn.Quote)));

            results.OrderBy(n => n.WhenCreated);

            return results;
        }

        public IEnumerable<SelectListItem> GetAssignmentSelectList()
        {
            IEnumerable<SelectListItem> results = context.Assignments.Select(a => new SelectListItem()
            {
                Value = a.ID.ToString(),
                Text = string.Format("{0} ({1})", a.Name, a.Site.AddressLine1)
            });

            return results;
        }
    }
}
