using System;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        public BillRepository(TransITDBContext context)
               : base(context)
        {
        }

        public override Expression<Func<Bill, bool>> MakeFilteringExpression(string keyword)
        {
            var predicate = PredicateBuilder.New<Bill>();
            if (decimal.TryParse(keyword, out decimal parsedDecimal))
            {
                predicate = predicate.And(entity =>
                    entity.Sum == parsedDecimal
                );
            }
            return predicate;
        }

        protected override IQueryable<Bill> ComplexEntities => Entities.
           Include(t => t.Create).
           Include(a => a.Document).
           Include(c => c.Issue).
           Include(v => v.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
