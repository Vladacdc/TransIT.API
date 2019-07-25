using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.Models;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class TokenRepository : BaseRepository<int, Token>, ITokenRepository
    {
        public TokenRepository(TransITDBContext context)
               : base(context)
        {
        }

        public override Task<IQueryable<Token>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.RefreshToken.Contains(str)))
                );
        
        protected override IQueryable<Token> ComplexEntities => Entities.
                   Include(t => t.Create).
                   Include(z => z.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
