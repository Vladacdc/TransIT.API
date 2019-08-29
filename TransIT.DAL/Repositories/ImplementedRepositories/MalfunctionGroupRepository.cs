using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class MalfunctionGroupRepository : BaseRepository<MalfunctionGroup>, IMalfunctionGroupRepository
    {
        public MalfunctionGroupRepository(TransITDBContext context)
               : base(context)
        {
        }

        public override Expression<Func<MalfunctionGroup, bool>> MakeFilteringExpression(string keyword)
        {
            return entity => entity.Name != null && 
                             entity.Name != string.Empty &&
                             EF.Functions.Like(entity.Name, '%' + keyword + '%');
        }

        protected override IQueryable<MalfunctionGroup> ComplexEntities => Entities.
                   Include(t => t.Create).
                   Include(z => z.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);          
    }
}
