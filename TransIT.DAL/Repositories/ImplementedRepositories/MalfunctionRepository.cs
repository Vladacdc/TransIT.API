using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionRepository : BaseRepository<Malfunction>, IMalfunctionRepository
    {
        public MalfunctionRepository(DbContext context)
               : base(context)
        {
        }
        
        public override Task<IQueryable<Malfunction>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)))
                );

        protected override IQueryable<Malfunction> ComplexEntities => Entities
                    .Include(m => m.Create)
                    .Include(m => m.Mod)
                    .Include(m => m.MalfunctionSubgroup)
                        .ThenInclude(s => s.MalfunctionGroup).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate);
    }
}
