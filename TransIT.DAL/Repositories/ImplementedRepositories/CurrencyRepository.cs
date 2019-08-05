using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(TransITDBContext context)
            : base(context)
        {
        }

        public override Task<IQueryable<Currency>> SearchExpressionAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<Currency>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                predicate = predicate.And(entity =>
                        EF.Functions.Like(entity.ShortName, '%' + temp + '%')
                    );
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }
    }
}
