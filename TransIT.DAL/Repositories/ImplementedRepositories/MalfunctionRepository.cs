using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionRepository : BaseRepository<Malfunction>, IMalfunctionRepository
    {
        public MalfunctionRepository(TransITDBContext context)
               : base(context)
        {
        }

        public override Expression<Func<Malfunction, bool>> MakeFilteringExpression(string keyword)
        {
            return entity => entity.Name != null &&
                             entity.Name != string.Empty &&
                             EF.Functions.Like(entity.Name, '%' + keyword + '%');
        }

        protected override IQueryable<Malfunction> ComplexEntities => Entities
                    .Include(m => m.Create)
                    .Include(m => m.Mod)
                    .Include(m => m.MalfunctionSubgroup)
                        .ThenInclude(s => s.MalfunctionGroup).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
