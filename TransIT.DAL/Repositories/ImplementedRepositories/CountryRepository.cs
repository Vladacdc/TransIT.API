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
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(TransITDBContext context)
            : base(context)
        {
        }

        public override Expression<Func<Country, bool>> MakeFilteringExpression(string keyword)
        {
            return entity => EF.Functions.Like(entity.Name, '%' + keyword + '%');
        }
    }
}
