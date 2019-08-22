using System;
using System.Collections.Generic;
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
    public class IssueLogRepository : BaseRepository<IssueLog>, IIssueLogRepository
    {
        public IssueLogRepository(TransITDBContext context)
            : base(context)
        {
        } 

        public override Expression<Func<IssueLog, bool>> MakeFilteringExpression(string keyword)
        {
            Expression<Func<IssueLog, bool>> expression =
                entity =>
                       entity.Description != null && entity.Description != string.Empty &&
                           EF.Functions.Like(entity.Description, '%' + keyword + '%')
                    || entity.NewState.TransName != null && entity.NewState.TransName != string.Empty &&
                           EF.Functions.Like(entity.NewState.TransName, '%' + keyword + '%')
                    || entity.OldState.TransName != null && entity.OldState.TransName != string.Empty &&
                           EF.Functions.Like(entity.OldState.TransName, '%' + keyword + '%')
                    || entity.ActionType.Name != null && entity.ActionType.Name != string.Empty &&
                           EF.Functions.Like(entity.ActionType.Name, '%' + keyword + '%')
                    || entity.Issue.Vehicle.InventoryId != null && entity.Issue.Vehicle.InventoryId != string.Empty &&
                           EF.Functions.Like(entity.Issue.Vehicle.InventoryId, '%' + keyword + '%');

            if (decimal.TryParse(keyword, out decimal parsedDecimal))
            { 
                Expression<Func<IssueLog, bool>> integerExpression =
                    entity => entity.Expenses != null && entity.Expenses.Value == parsedDecimal;

                expression = PredicateBuilder.New<IssueLog>().And(expression).And(integerExpression);
            }

            return expression;
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
