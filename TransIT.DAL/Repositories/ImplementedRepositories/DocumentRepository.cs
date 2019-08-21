﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(TransITDBContext context)
            : base(context)
        {
        }

        public override Task<IQueryable<Document>> SearchAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<Document>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                predicate = predicate.And(entity =>
                       entity.Name != null && entity.Name != string.Empty &&
                           EF.Functions.Like(entity.Name, '%' + temp + '%')
                    || entity.Description != null && entity.Description != string.Empty &&
                           EF.Functions.Like(entity.Description, '%' + temp + '%')
                    );
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }

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
