using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionGroupRepository : BaseRepository<MalfunctionGroup>, IMalfunctionGroupRepository
    {
        public MalfunctionGroupRepository(DbContext context)
               : base(context)
        {
        }
        
        public override Task<IQueryable<MalfunctionGroup>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)))
                );

        protected override IQueryable<MalfunctionGroup> ComplexEntities => Entities.
                   Include(t => t.Create).
                   Include(z => z.Mod).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate);          
    }
}
