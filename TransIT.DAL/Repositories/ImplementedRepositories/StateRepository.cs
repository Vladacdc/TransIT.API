using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.Models;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class StateRepository : BaseRepository<int, State>, IStateRepository
    {
        public StateRepository(TransITDBContext context)
               : base(context)
        {
        }
        
        public override Task<IQueryable<State>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.TransName.ToUpperInvariant().Contains(str)))
                );
    }
}
