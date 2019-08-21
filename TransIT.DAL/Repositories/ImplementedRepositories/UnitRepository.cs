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
    public class UnitRepository : BaseRepository<Unit>, IUnitRepository
    {
        public UnitRepository(TransITDBContext context) : base(context)
        {
        }

        public override Task<IQueryable<Unit>> SearchAsync(IEnumerable<string> strArr)
        {
            var predicate = PredicateBuilder.New<Unit>();

            foreach (var keyword in strArr)
            {
                predicate = predicate.And(entity =>
                    EF.Functions.Like(entity.Name, '%' + keyword + '%') ||
                    EF.Functions.Like(entity.ShortName, "%" + keyword + "%")
                    );
            }

            return Task.FromResult(
                GetQueryable()
                    .AsExpandable()
                    .Where(predicate)
            );
        }

        protected override IQueryable<Unit> ComplexEntities
        {
            get
            {
                return Entities.Include(u => u.Create)
                    .Include(u => u.Mod)
                    .OrderByDescending(u => u.UpdatedDate)
                    .ThenByDescending(u => u.CreatedDate);
            }
        }
    }
}