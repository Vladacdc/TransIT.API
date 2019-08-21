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
    public class PartRepository : BaseRepository<Part>, IPartRepository
    {
        public PartRepository(TransITDBContext context)
               : base(context)
        {
        }

        public override Task<IQueryable<Part>> SearchExpressionAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<Part>();

            foreach (string keyword in strs)
            {
                predicate = predicate.And(entity =>
                   EF.Functions.Like(entity.Name, '%' + keyword + '%')
                   );
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }
        
        protected override IQueryable<Part> ComplexEntities => Entities.
           Include(p => p.Create).
           Include(p => p.Mod).
           Include(p => p.Unit).
           Include(p => p.Manufacturer).
           OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
