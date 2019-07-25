using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.Models;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionSubgroupRepository : BaseRepository<int, MalfunctionSubgroup>, IMalfunctionSubgroupRepository
    {
        public MalfunctionSubgroupRepository(TransITDBContext context)
            : base(context)
        {
        }
        
        public override Task<IQueryable<MalfunctionSubgroup>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)))
            );

        protected override IQueryable<MalfunctionSubgroup> ComplexEntities => Entities
                   .Include(t => t.Create)
                   .Include(z => z.Mod)
                   .Include(a => a.MalfunctionGroup).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
