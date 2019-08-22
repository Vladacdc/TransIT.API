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
    public class WorkTypeRepository :BaseRepository<WorkType>,IWorkTypeRepository
    {
        public WorkTypeRepository(TransITDBContext context) : base(context)
        {
        }

        public override Expression<Func<WorkType, bool>> MakeFilteringExpression(string keyword)
        {
            return part => EF.Functions.Like(part.Name, '%' + keyword + '%');
        }

        protected override IQueryable<WorkType> ComplexEntities
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
