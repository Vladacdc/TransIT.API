using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
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

        public override Task<IQueryable<Transition>> SearchAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<Transition>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                predicate = predicate.And(entity =>
                       entity.FromState.TransName != null && entity.FromState.TransName != string.Empty &&
                           EF.Functions.Like(entity.FromState.TransName, '%' + temp + '%')
                    || entity.ToState.TransName != null && entity.ToState.TransName != string.Empty &&
                           EF.Functions.Like(entity.ToState.TransName, '%' + temp + '%')
                    || entity.ActionType.Name != null && entity.ActionType.Name != string.Empty &&
                           EF.Functions.Like(entity.ActionType.Name, '%' + temp + '%')
                    );
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }

        protected override IQueryable<Transition> ComplexEntities => Entities.
           Include(u => u.ActionType).
           Include(u => u.FromState).
           Include(u => u.ToState).
           Include(t => t.Create).
           Include(w => w.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
