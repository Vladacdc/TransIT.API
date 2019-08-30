using System;
using System.Linq;
using System.Linq.Expressions;
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

        public override Expression<Func<Issue, bool>> MakeFilteringExpression(string keyword)
        {
            Expression<Func<Issue, bool>> expression =
                entity =>
                       EF.Functions.Like(entity.Summary, '%' + keyword + '%')
                    || EF.Functions.Like(entity.Malfunction.Name, '%' + keyword + '%')
                    || EF.Functions.Like(entity.Malfunction.MalfunctionSubgroup.Name, '%' + keyword + '%')
                    || EF.Functions.Like(entity.Malfunction.MalfunctionSubgroup.MalfunctionGroup.Name,
                         '%' + keyword + '%')
                    || entity.State.TransName != null && entity.State.TransName != string.Empty &&
                           EF.Functions.Like(entity.State.TransName, '%' + keyword + '%')
                    || entity.Vehicle.Brand != null && entity.Vehicle.Brand != string.Empty &&
                           EF.Functions.Like(entity.Vehicle.Brand, '%' + keyword + '%')
                    || entity.Vehicle.InventoryId != null && entity.Vehicle.InventoryId != string.Empty &&
                           EF.Functions.Like(entity.Vehicle.InventoryId, '%' + keyword + '%')
                    || entity.Vehicle.Model != null && entity.Vehicle.Model != string.Empty &&
                           EF.Functions.Like(entity.Vehicle.Model, '%' + keyword + '%')
                    || entity.Vehicle.RegNum != null && entity.Vehicle.RegNum != string.Empty &&
                           EF.Functions.Like(entity.Vehicle.RegNum, '%' + keyword + '%')
                    || entity.Vehicle.Vincode != null && entity.Vehicle.Vincode != string.Empty &&
                           EF.Functions.Like(entity.Vehicle.Vincode, '%' + keyword + '%');

            if (int.TryParse(keyword, out int parsedInteger))
            {
                Expression<Func<Issue, bool>> integerExpression =
                    entity => entity.Number == parsedInteger
                        || entity.Date.Year == parsedInteger
                        || entity.Date.Month == parsedInteger
                        || entity.Date.Day == parsedInteger;

                expression = PredicateBuilder.New<Issue>().And(expression).And(integerExpression);
            }

            return expression;
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
