using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class TransitionRepository:BaseRepository<Transition>, ITransitionRepository
    {
        public TransitionRepository(TransITDBContext context)
               : base(context)
        {
        }

        public override Expression<Func<Transition, bool>> MakeFilteringExpression(string keyword)
        {
            return entity =>
                   entity.FromState.TransName != null && entity.FromState.TransName != string.Empty &&
                       EF.Functions.Like(entity.FromState.TransName, '%' + keyword + '%')
                || entity.ToState.TransName != null && entity.ToState.TransName != string.Empty &&
                       EF.Functions.Like(entity.ToState.TransName, '%' + keyword + '%')
                || entity.ActionType.Name != null && entity.ActionType.Name != string.Empty &&
                       EF.Functions.Like(entity.ActionType.Name, '%' + keyword + '%');
        }

        protected override IQueryable<Transition> ComplexEntities => Entities.
           Include(u => u.ActionType).
           Include(u => u.FromState).
           Include(u => u.ToState).
           Include(t => t.Create).
           Include(w => w.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
