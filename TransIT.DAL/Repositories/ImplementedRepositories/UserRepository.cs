using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class UserRepository : BaseRepository<string, User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }
        
        public override Task<IQueryable<User>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.UserName.ToUpperInvariant().Contains(str)
                    || entity.Email.ToUpperInvariant().Contains(str)
                    || entity.PhoneNumber.ToUpperInvariant().Contains(str)
                    || entity.LastName.ToUpperInvariant().Contains(str)
                    || entity.FirstName.ToUpperInvariant().Contains(str)))
                );

        protected override IQueryable<User> ComplexEntities => Entities.
            Include(a => a.CreatedBy).
            Include(b => b.ModifiedBy).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
