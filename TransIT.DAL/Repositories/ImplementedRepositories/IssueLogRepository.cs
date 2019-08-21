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
    public class IssueLogRepository : BaseRepository<IssueLog>, IIssueLogRepository
    {
        public IssueLogRepository(TransITDBContext context)
            : base(context)
        {
        } 

        public override Task<IQueryable<IssueLog>> SearchExpressionAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<IssueLog>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                predicate = predicate.And(entity =>
                       entity.Description != null && entity.Description != string.Empty &&
                           EF.Functions.Like(entity.Description, '%' + temp + '%')
                    || entity.NewState.TransName != null && entity.NewState.TransName != string.Empty &&
                           EF.Functions.Like(entity.NewState.TransName, '%' + temp + '%')
                    || entity.OldState.TransName != null && entity.OldState.TransName != string.Empty &&
                           EF.Functions.Like(entity.OldState.TransName, '%' + temp + '%')
                    || entity.ActionType.Name != null && entity.ActionType.Name != string.Empty &&
                           EF.Functions.Like(entity.ActionType.Name, '%' + temp + '%')
                    || entity.Issue.Vehicle.InventoryId != null && entity.Issue.Vehicle.InventoryId != string.Empty &&
                           EF.Functions.Like(entity.Issue.Vehicle.InventoryId, '%' + temp + '%')
                    );

                if (decimal.TryParse(temp, out decimal parsedDecimal))
                {
                    predicate = predicate.And(
                        entity => entity.Expenses != null && entity.Expenses.Value == parsedDecimal
                    );
                }
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }

        protected override IQueryable<IssueLog> ComplexEntities => Entities
            .Include(t => t.ActionType)
            .Include(z => z.Create)
            .Include(a => a.Issue)
            .ThenInclude(x => x.Vehicle)
            .Include(x => x.Issue)
            .ThenInclude(x => x.Malfunction)
            .Include(x => x.Issue)
            .ThenInclude(x => x.AssignedTo)
            .Include(x => x.Issue)
            .ThenInclude(x => x.State)
            .Include(b => b.Mod)
            .Include(c => c.NewState)
            .Include(d => d.OldState)
            .Include(e => e.Supplier)
            .Include(x => x.Document)
            .Include(x => x.IssueLogParts)
            .OrderByDescending(u => u.UpdatedDate)
            .ThenByDescending(x => x.CreatedDate);
    }
}
