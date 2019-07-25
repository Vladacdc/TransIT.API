using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.Models;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class SupplierRepository : BaseRepository<int, Supplier>, ISupplierRepository
    {
        public SupplierRepository(TransITDBContext context)
               : base(context)
        {
        }

        public override Task<IQueryable<Supplier>> SearchExpressionAsync(IEnumerable<string> strs) =>
             Task.FromResult(
                 GetQueryable().Where(entity =>
                     strs.Any(str => !string.IsNullOrEmpty(entity.Name) && entity.Name.ToUpperInvariant().Contains(str)
                     || !string.IsNullOrEmpty(entity.Edrpou) && entity.Edrpou.ToUpperInvariant().Contains(str)
                     || !string.IsNullOrEmpty(entity.FullName) && entity.FullName.ToUpperInvariant().Contains(str)
                     || !string.IsNullOrEmpty(entity.Country.Name) && entity.Country.Name.ToUpperInvariant().Contains(str)
                     || !string.IsNullOrEmpty(entity.Currency.FullName) && entity.Currency.FullName.ToUpperInvariant().Contains(str)))
                 );

        protected override IQueryable<Supplier> ComplexEntities => Entities
                   .Include(t => t.Create)
                   .Include(z => z.Mod)
                   .Include(c => c.Currency)
                   .Include(c => c.Country)
                   .OrderByDescending(u => u.UpdatedDate)
                   .ThenByDescending(x => x.CreatedDate);
    }
}
