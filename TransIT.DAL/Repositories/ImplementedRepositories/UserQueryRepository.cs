using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
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

        public Task<IQueryable<User>> SearchExpressionAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<User>();

            foreach (string keyword in strs)
            {
                string temp = keyword;

                predicate = predicate.And(entity =>
                       entity.FirstName != null && entity.FirstName != string.Empty &&
                           EF.Functions.Like(entity.FirstName, '%' + temp + '%')
                    || entity.MiddleName != null && entity.MiddleName != string.Empty &&
                           EF.Functions.Like(entity.MiddleName, '%' + temp + '%')
                    || entity.LastName != null && entity.LastName != string.Empty &&
                           EF.Functions.Like(entity.LastName, '%' + temp + '%')
                    || entity.NormalizedUserName != null && entity.NormalizedUserName != string.Empty &&
                           EF.Functions.Like(entity.NormalizedUserName, '%' + temp + '%')
                    || entity.NormalizedEmail != null && entity.NormalizedEmail != string.Empty &&
                           EF.Functions.Like(entity.NormalizedEmail, '%' + temp + '%')
                    || entity.PhoneNumber != null && entity.PhoneNumber != string.Empty &&
                           EF.Functions.Like(entity.PhoneNumber, '%' + temp + '%'));
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }

    }
}
