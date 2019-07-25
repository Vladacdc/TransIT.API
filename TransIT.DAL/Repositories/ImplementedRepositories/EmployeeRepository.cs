using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context)
               : base(context)
        {
        }

        public override Task<IQueryable<Employee>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Post.Name.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.FirstName) && entity.FirstName.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.MiddleName) && entity.MiddleName.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.LastName) && entity.LastName.ToUpperInvariant().Contains(str)
                    || entity.BoardNumber.ToString().ToUpperInvariant().Contains(str)
                    || entity.ShortName.ToUpperInvariant().Contains(str))));
        
        protected override IQueryable<Employee> ComplexEntities => Entities
            .Include(e => e.Create)
            .Include(e => e.Mod).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate)
            .Include(e => e.Post);
    }
}
