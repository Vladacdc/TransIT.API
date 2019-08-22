using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
 
        public override Expression<Func<Supplier, bool>> MakeFilteringExpression(string keyword)
        {
            return entity =>
                   entity.Name != null && entity.Name != string.Empty &&
                       EF.Functions.Like(entity.Name, '%' + keyword + '%')
                || entity.Edrpou != null && entity.Edrpou != string.Empty &&
                       EF.Functions.Like(entity.Edrpou, '%' + keyword + '%')
                || entity.FullName != null && entity.FullName != string.Empty &&
                       EF.Functions.Like(entity.FullName, '%' + keyword + '%')
                || entity.Country.Name != null && entity.Country.Name != string.Empty &&
                       EF.Functions.Like(entity.Country.Name, '%' + keyword + '%')
                || entity.Currency.FullName != null && entity.Currency.FullName != string.Empty &&
                       EF.Functions.Like(entity.Currency.FullName, '%' + keyword + '%');
        }

        protected override IQueryable<Supplier> ComplexEntities => Entities
                   .Include(t => t.Create)
                   .Include(z => z.Mod)
                   .Include(c => c.Currency)
                   .Include(c => c.Country)
                   .Include(c => c.SuppliersParts)
                   .OrderByDescending(u => u.UpdatedDate)
                   .ThenByDescending(x => x.CreatedDate);
    }
}
