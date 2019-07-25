using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        public BillRepository(DbContext context)
               : base(context)
        {
        }

        public override Task<IQueryable<Bill>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Sum.ToString().Contains(str)))
            );
        
        protected override IQueryable<Bill> ComplexEntities => Entities.
           Include(t => t.Create).
           Include(a => a.Document).
           Include(c => c.Issue).
           Include(v => v.Mod).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate);
    }
}
