using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransIT.DAL.Repositories
{
    public interface IQueryRepository<TEntity>
    {
        IQueryable<TEntity> GetQueryable();
        Task<IQueryable<TEntity>> SearchExpressionAsync(IEnumerable<string> strs);
    }
}
