using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class UserQueryRepository : IQueryRepository<User>
    {
        private readonly TransITDBContext _transITDBContext;
        private IQueryable<User> _users;

        public UserQueryRepository(TransITDBContext transITDBContext)
        {
            _transITDBContext = transITDBContext;
            _users = _transITDBContext.Set<User>()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsQueryable();

        }

        public IQueryable<User> GetQueryable()
        {
            return _users;
        }

        /// <summary>
        /// Search is only by UserName !!!
        /// Need to fix.
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public Task<IQueryable<User>> SearchExpressionAsync(IEnumerable<string> strs)
        {
            //need to fix
            return Task.FromResult(
               GetQueryable().Where(entity => entity.UserName == strs.FirstOrDefault()));
        }
    }
}
