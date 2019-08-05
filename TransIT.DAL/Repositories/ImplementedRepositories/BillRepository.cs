using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public override Task<IQueryable<Bill>> SearchExpressionAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<Bill>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                if (decimal.TryParse(temp, out decimal parsedDecimal))
                {
                    predicate = predicate.And(entity =>
                        entity.Sum == parsedDecimal
                    );
                }
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }
        
        protected override IQueryable<Bill> ComplexEntities => Entities.
           Include(t => t.Create).
           Include(a => a.Document).
           Include(c => c.Issue).
           Include(v => v.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
