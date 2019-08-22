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
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(TransITDBContext context)
            : base(context)
        {
        }

        public override Expression<Func<Location, bool>> MakeFilteringExpression(string keyword)
        {
            return entity =>
                   entity.Name != null && entity.Name != string.Empty &&
                       EF.Functions.Like(entity.Name, '%' + keyword + '%')
                || entity.Description != null && entity.Description != string.Empty &&
                       EF.Functions.Like(entity.Description, '%' + keyword + '%');
        }

        protected override IQueryable<Location> ComplexEntities => Entities.
            Include(a => a.Create).
            Include(b => b.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
