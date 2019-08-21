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
    public class WorkTypeRepository :BaseRepository<WorkType>,IWorkTypeRepository
    {
        public WorkTypeRepository(TransITDBContext context) : base(context)
        {
        }

        public override Task<IQueryable<WorkType>> SearchExpressionAsync(IEnumerable<string> strArr)
        {
            var predicate = PredicateBuilder.New<WorkType>();

            foreach (var keyword in strArr)
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

        protected override IQueryable<WorkType> ComplexEntities
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
