using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionSubgroupRepository : BaseRepository<MalfunctionSubgroup>, IMalfunctionSubgroupRepository
    {
        public MalfunctionSubgroupRepository(TransITDBContext context)
            : base(context)
        {
        }

        public override Expression<Func<MalfunctionSubgroup, bool>> MakeFilteringExpression(string keyword)
        {
            return entity => entity.Name != null &&
                             entity.Name != string.Empty &&
                             EF.Functions.Like(entity.Name, '%' + keyword + '%');
        }

        protected override IQueryable<MalfunctionSubgroup> ComplexEntities => Entities
                   .Include(t => t.Create)
                   .Include(z => z.Mod)
                   .Include(a => a.MalfunctionGroup).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
