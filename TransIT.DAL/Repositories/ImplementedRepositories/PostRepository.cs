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
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(TransITDBContext context)
               : base(context)
        {
        }

        public override Expression<Func<Post, bool>> MakeFilteringExpression(string keyword)
        {
            return entity => entity.Name != null &&
                             entity.Name != string.Empty &&
                             EF.Functions.Like(entity.Name, '%' + keyword + '%');
        }

        protected override IQueryable<Post> ComplexEntities => Entities
            .Include(p => p.Create)
            .Include(p => p.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
