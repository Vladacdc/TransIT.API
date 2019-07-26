using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.Models;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(TransITDBContext context)
            : base(context)
        {
        }
        
        public override Task<IQueryable<Document>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)
                    || entity.Description.ToUpperInvariant().Contains(str)))
                );

        protected override IQueryable<Document> ComplexEntities => Entities
            .Include(t => t.Create)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.Issue.State)
            .Include(x => x.IssueLog)
            .ThenInclude(x => x.Issue.Vehicle)
            .ThenInclude(x => x.VehicleType)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.Supplier)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.ActionType)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.Issue.Malfunction)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.NewState)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.OldState)
            .Include(z => z.IssueLog)
            .ThenInclude(x => x.Issue.AssignedTo)
            .Include(a => a.Mod)
            .OrderByDescending(u => u.UpdatedDate)
            .ThenByDescending(x => x.CreatedDate);
    }
}
