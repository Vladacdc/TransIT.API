using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class PartsInRepository : BaseRepository<PartIn>, IPartsInRepository
    {
        public PartsInRepository(TransITDBContext context) : base(context)
        {
        }

        public override Task<IQueryable<PartIn>> SearchExpressionAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<PartIn>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                predicate = predicate.And(entity =>
                       entity.Batch != null && entity.Batch != string.Empty &&
                           EF.Functions.Like(entity.Batch, '%' + temp + '%')
                    || entity.Currency.FullName != null && entity.Currency.FullName != string.Empty &&
                           EF.Functions.Like(entity.Currency.FullName, '%' + temp + '%')
                    || entity.Currency.ShortName != null && entity.Currency.ShortName != string.Empty &&
                           EF.Functions.Like(entity.Currency.ShortName, '%' + temp + '%')
                    );
                if (int.TryParse(temp, out int integer))
                {
                    predicate = predicate.And(entity =>
                           (int)entity.Price == integer 
                        || entity.Amount == integer
                        || entity.ArrivalDate.Year == integer
                        || entity.ArrivalDate.Month == integer
                        || entity.ArrivalDate.Day == integer
                    );
                }
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }
    }
}
