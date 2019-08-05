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
    public class MalfunctionRepository : BaseRepository<Malfunction>, IMalfunctionRepository
    {
        public MalfunctionRepository(TransITDBContext context)
               : base(context)
        {
        }
        
         public override Task<IQueryable<Malfunction>> SearchExpressionAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<Malfunction>();

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

        protected override IQueryable<Malfunction> ComplexEntities => Entities
                    .Include(m => m.Create)
                    .Include(m => m.Mod)
                    .Include(m => m.MalfunctionSubgroup)
                        .ThenInclude(s => s.MalfunctionGroup).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
