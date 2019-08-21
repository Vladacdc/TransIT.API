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
    public class ActionTypeRepository : BaseRepository<ActionType>, IActionTypeRepository
    {
        public ActionTypeRepository(TransITDBContext context)
               : base(context)
        {
        }

         
        protected override IQueryable<ActionType> ComplexEntities => Entities.
           Include(t => t.Create).
           Include(w => w.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);

        public override Expression<Func<ActionType, bool>> MakeFilteringExpression(string keyword)
        {
            return entity => EF.Functions.Like(entity.Name, '%' + keyword + '%');
        }
    }
}
