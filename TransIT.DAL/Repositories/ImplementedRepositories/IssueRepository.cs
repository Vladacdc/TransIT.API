using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class IssueRepository : BaseRepository<Issue>, IIssueRepository
    {
        public IssueRepository(DbContext context)
               : base(context)
        {
        }

        public override async Task<Issue> AddAsync(Issue issue)
        {
            int previousMax = await Entities.DefaultIfEmpty().MaxAsync(i => i.Number) ?? 0;
            issue.Number = previousMax + 1;

            return await base.AddAsync(issue);
        }

        public override Task<IQueryable<Issue>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Summary.ToUpperInvariant().Contains(str)
                    || entity.Malfunction.Name.ToUpperInvariant().Contains(str)
                    || entity.Malfunction.MalfunctionSubgroup.Name.ToUpperInvariant().Contains(str)
                    || entity.Malfunction.MalfunctionSubgroup.MalfunctionGroup.Name.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.State.TransName) && entity.State.TransName.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Vehicle.Brand) && entity.Vehicle.Brand.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Vehicle.InventoryId) && entity.Vehicle.InventoryId.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Vehicle.Model) && entity.Vehicle.Model.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Vehicle.RegNum) && entity.Vehicle.RegNum.ToUpperInvariant().Contains(str)
                    || !string.IsNullOrEmpty(entity.Vehicle.Vincode) && entity.Vehicle.Vincode.ToUpperInvariant().Contains(str)
                    || entity.Date.ToString("yyyy-MM-dd").Contains(str)
                    || entity.Number.ToString().ToUpperInvariant().Contains(str)))
                );
        
        protected override IQueryable<Issue> ComplexEntities => Entities
            .Include(i => i.AssignedTo)
            .Include(i => i.Create)
            .Include(i => i.Malfunction)
                .ThenInclude(m => m.MalfunctionSubgroup)
                    .ThenInclude(s => s.MalfunctionGroup)
            .Include(i => i.Mod)
            .Include(i => i.State)
            .Include(i => i.Vehicle)
                .ThenInclude(n => n.Location).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate);        

    }
}
