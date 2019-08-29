using System;
using System.Linq.Expressions;
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

        public override Expression<Func<State, bool>> MakeFilteringExpression(string keyword)
        {
            return entity => entity.TransName != null &&
                             entity.TransName != string.Empty &&
                             EF.Functions.Like(entity.TransName, '%' + keyword + '%');
        }
    }
}
