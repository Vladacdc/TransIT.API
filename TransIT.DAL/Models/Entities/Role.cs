using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Role : IdentityRole<int>, IAuditableEntity
    {
        public Role()
        {
            User = new HashSet<User>();
        }
        
        public string TransName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
