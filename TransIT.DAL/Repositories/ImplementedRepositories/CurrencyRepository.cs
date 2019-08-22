using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public override Expression<Func<Currency, bool>> MakeFilteringExpression(string keyword)
        {
            return entity => EF.Functions.Like(entity.ShortName, '%' + keyword + '%');
        }
    }
}
