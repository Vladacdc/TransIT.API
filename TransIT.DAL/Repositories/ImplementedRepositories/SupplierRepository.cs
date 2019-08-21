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
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(TransITDBContext context)
               : base(context)
        {
        }
 
        public override Task<IQueryable<Supplier>> SearchAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<Supplier>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                predicate = predicate.And(entity =>
                       entity.Name != null && entity.Name != string.Empty &&
                           EF.Functions.Like(entity.Name, '%' + temp + '%')
                    || entity.Edrpou != null && entity.Edrpou != string.Empty &&
                           EF.Functions.Like(entity.Edrpou, '%' + temp + '%')
                    || entity.FullName != null && entity.FullName != string.Empty &&
                           EF.Functions.Like(entity.FullName, '%' + temp + '%')
                    || entity.Country.Name != null && entity.Country.Name != string.Empty &&
                           EF.Functions.Like(entity.Country.Name, '%' + temp + '%')
                    || entity.Currency.FullName != null && entity.Currency.FullName != string.Empty &&
                           EF.Functions.Like(entity.Currency.FullName, '%' + temp + '%')
                    );
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }

        protected override IQueryable<Supplier> ComplexEntities => Entities
                   .Include(t => t.Create)
                   .Include(z => z.Mod)
                   .Include(c => c.Currency)
                   .Include(c => c.Country)
                   .OrderByDescending(u => u.UpdatedDate)
                   .ThenByDescending(x => x.CreatedDate);
    }
}
