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
    public class PartRepository : BaseRepository<Part>, IPartRepository
    {
        public PartRepository(TransITDBContext context)
               : base(context)
        {
        }
        
        public override Expression<Func<Part, bool>> MakeFilteringExpression(string keyword)
        {
            return part => EF.Functions.Like(part.Name, '%' + keyword + '%');
        }

        protected override IQueryable<Part> ComplexEntities => Entities.
           Include(p => p.Create).
           Include(p => p.Mod).
           Include(p => p.Unit).
           Include(p => p.Manufacturer).
           Include(p => p.SuppliersParts).
           ThenInclude(x => x.Supplier).
           OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
