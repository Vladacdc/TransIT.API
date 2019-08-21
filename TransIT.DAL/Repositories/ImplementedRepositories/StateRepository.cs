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
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(TransITDBContext context)
               : base(context)
        {
        }
        
         public override Task<IQueryable<State>> SearchAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<State>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                predicate = predicate.And(entity =>
                       entity.TransName != null && entity.TransName != string.Empty &&
                           EF.Functions.Like(entity.TransName, '%' + temp + '%')
                    );
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }
    }
}
