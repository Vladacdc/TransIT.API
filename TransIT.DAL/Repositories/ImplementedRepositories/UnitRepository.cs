using System;
using System.Linq;
using System.Linq.Expressions;
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

        public override Expression<Func<Unit, bool>> MakeFilteringExpression(string keyword)
        {
            return entity =>
                   EF.Functions.Like(entity.Name, '%' + keyword + '%') ||
                   EF.Functions.Like(entity.ShortName, "%" + keyword + "%");
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