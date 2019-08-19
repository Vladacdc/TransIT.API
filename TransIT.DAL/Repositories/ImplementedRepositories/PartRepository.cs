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
                string temp = keyword;
                if (decimal.TryParse(temp, out decimal parsedDecimal))
                {
                    //TODO
                    //predicate = predicate.And(entity =>
                    //    entity.Sum == parsedDecimal
                    //);
                }
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
            //TODO
           OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
