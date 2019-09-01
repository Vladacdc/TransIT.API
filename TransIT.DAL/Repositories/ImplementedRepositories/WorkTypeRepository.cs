using System;
using System.Linq;
using System.Linq.Expressions;
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
            return workType => EF.Functions.Like(workType.Name, '%' + keyword + '%') || 
                           EF.Functions.Like(workType.EstimatedCost.ToString(), '%' + keyword + '%') || 
                           EF.Functions.Like(workType.EstimatedTime.ToString(), '%' + keyword + '%');
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
