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
    public class IssueRepository : BaseRepository<Issue>, IIssueRepository
    {
        public IssueRepository(TransITDBContext context)
               : base(context)
        {
        }

        public override async Task<Issue> AddAsync(Issue issue)
        {
            int previousMax = await Entities.DefaultIfEmpty().MaxAsync(i => i.Number) ?? 0;
            issue.Number = previousMax + 1;

            return await base.AddAsync(issue);
        } 

        public override Task<IQueryable<Issue>> SearchAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<Issue>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
               
                predicate = predicate.And(entity =>
                       EF.Functions.Like(entity.Summary, '%' + temp + '%')
                    || EF.Functions.Like(entity.Malfunction.Name, '%' + temp + '%')
                    || EF.Functions.Like(entity.Malfunction.MalfunctionSubgroup.Name, '%' + temp + '%')
                    || EF.Functions.Like(entity.Malfunction.MalfunctionSubgroup.MalfunctionGroup.Name, 
                         '%' + temp + '%')
                    || entity.State.TransName != null && entity.State.TransName != string.Empty &&
                           EF.Functions.Like(entity.State.TransName, '%' + temp + '%')
                    || entity.Vehicle.Brand != null && entity.Vehicle.Brand != string.Empty &&
                           EF.Functions.Like(entity.Vehicle.Brand, '%' + temp + '%')
                    || entity.Vehicle.InventoryId != null && entity.Vehicle.InventoryId != string.Empty &&
                           EF.Functions.Like(entity.Vehicle.InventoryId, '%' + temp + '%')
                    || entity.Vehicle.Model != null && entity.Vehicle.Model != string.Empty &&
                           EF.Functions.Like(entity.Vehicle.Model, '%' + temp + '%')
                    || entity.Vehicle.RegNum != null && entity.Vehicle.RegNum != string.Empty &&
                           EF.Functions.Like(entity.Vehicle.RegNum, '%' + temp + '%')
                    || entity.Vehicle.Vincode != null && entity.Vehicle.Vincode != string.Empty &&
                           EF.Functions.Like(entity.Vehicle.Vincode, '%' + temp + '%')
                    );
                if (int.TryParse(temp, out int parsedInteger))
                {
                    predicate = predicate.And(entity =>
                           entity.Number == parsedInteger
                        || entity.Date.Year == parsedInteger
                        || entity.Date.Month == parsedInteger
                        || entity.Date.Day == parsedInteger
                    );
                }
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }
        protected override IQueryable<Issue> ComplexEntities => Entities
            .Include(i => i.AssignedTo)
            .Include(i => i.Create)
            .Include(i => i.Malfunction)
                .ThenInclude(m => m.MalfunctionSubgroup)
                    .ThenInclude(s => s.MalfunctionGroup)
            .Include(i => i.Mod)
            .Include(i => i.State)
            .Include(i => i.Vehicle)
                .ThenInclude(n => n.Location).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);        

    }
}
