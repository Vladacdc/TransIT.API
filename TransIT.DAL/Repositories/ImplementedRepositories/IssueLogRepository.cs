using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class IssueLogRepository : BaseRepository<IssueLog>, IIssueLogRepository
    {
        public IssueLogRepository(DbContext context)
            : base(context)
        {
        }

        public override Task<IQueryable<IssueLog>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => !string.IsNullOrEmpty(entity.Description) && entity.Description.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.NewState.TransName) && entity.NewState.TransName.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.OldState.TransName) && entity.OldState.TransName.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Expenses.ToString()) && entity.Expenses.ToString().ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.ActionType.Name) && entity.ActionType.Name.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Issue.Vehicle.InventoryId) && entity.Issue.Vehicle.InventoryId.ToUpperInvariant().Contains(str)))
                );

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
            .OrderByDescending(u => u.ModDate)
            .ThenByDescending(x => x.CreateDate);
    }
}
