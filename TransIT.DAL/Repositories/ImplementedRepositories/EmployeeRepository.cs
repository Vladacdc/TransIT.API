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
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(TransITDBContext context)
               : base(context)
        {
        }

        public override Task<IQueryable<Employee>> SearchExpressionAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<Employee>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                predicate = predicate.And(entity =>
                       EF.Functions.Like(entity.Post.Name, '%' + temp + '%')
                    || entity.FirstName != null && entity.FirstName != string.Empty &&
                           EF.Functions.Like(entity.FirstName, '%' + temp + '%')
                    || entity.MiddleName != null && entity.MiddleName != string.Empty &&
                           EF.Functions.Like(entity.MiddleName, '%' + temp + '%')
                    || entity.LastName != null && entity.LastName != string.Empty &&
                           EF.Functions.Like(entity.LastName, '%' + temp + '%')
                    || EF.Functions.Like(entity.ShortName, '%' + temp + '%')
                    );

                if (int.TryParse(temp, out int parsedInteger))
                {
                    predicate = predicate.And(
                        entity => entity.BoardNumber == parsedInteger
                    );
                }
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }

        protected override IQueryable<Employee> ComplexEntities => Entities
            .Include(e => e.Create)
            .Include(e => e.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate)
            .Include(e => e.Post);
    }
}
