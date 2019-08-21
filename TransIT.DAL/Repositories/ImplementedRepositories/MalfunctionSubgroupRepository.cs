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
    public class MalfunctionSubgroupRepository : BaseRepository<MalfunctionSubgroup>, IMalfunctionSubgroupRepository
    {
        public MalfunctionSubgroupRepository(TransITDBContext context)
            : base(context)
        {
        }
        
         public override Task<IQueryable<MalfunctionSubgroup>> SearchAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<MalfunctionSubgroup>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                predicate = predicate.And(entity =>
                       entity.Name != null && entity.Name != string.Empty &&
                           EF.Functions.Like(entity.Name, '%' + temp + '%')
                    );
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }

        protected override IQueryable<MalfunctionSubgroup> ComplexEntities => Entities
                   .Include(t => t.Create)
                   .Include(z => z.Mod)
                   .Include(a => a.MalfunctionGroup).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
