using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.Models;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(TransITDBContext context)
               : base(context)
        {
        }
        
        public override Task<IQueryable<Post>> SearchExpressionAsync(IEnumerable<string> strs) =>
            Task.FromResult(
                GetQueryable().Where(entity =>
                    strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)))
                );

        protected override IQueryable<Post> ComplexEntities => Entities
            .Include(p => p.Create)
            .Include(p => p.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
