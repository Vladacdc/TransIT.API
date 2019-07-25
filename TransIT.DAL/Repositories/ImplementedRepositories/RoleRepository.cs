<<<<<<< HEAD
﻿//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using System.Threading.Tasks;
//using TransIT.DAL.Models.Entities;
//using TransIT.DAL.Repositories.InterfacesRepositories;

//namespace TransIT.DAL.Repositories.ImplementedRepositories
//{
//    public class RoleRepository : BaseRepository<Role>, IRoleRepository
//    {
//        public RoleRepository(DbContext context)
//               : base(context)
//        {
//        }
=======
﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.Models;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class RoleRepository : BaseRepository<string, Role>, IRoleRepository
    {
        public RoleRepository(TransITDBContext context)
               : base(context)
        {
        }
>>>>>>> fdf9cbfad24a7fb04b4b63d879da8b685ae1e277
        
//        public override Task<IQueryable<Role>> SearchExpressionAsync(IEnumerable<string> strs) =>
//            Task.FromResult(
//                GetQueryable().Where(entity =>
//                    strs.Any(str => entity.Name.ToUpperInvariant().Contains(str)))
//                );

<<<<<<< HEAD
//        protected override IQueryable<Role> ComplexEntities => Entities.
//                   Include(t => t.Create).
//                   Include(z => z.Mod).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate);
//    }
//}
=======
        protected override IQueryable<Role> ComplexEntities => Entities.
                   Include(t => t.Create).
                   Include(z => z.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
>>>>>>> fdf9cbfad24a7fb04b4b63d879da8b685ae1e277
