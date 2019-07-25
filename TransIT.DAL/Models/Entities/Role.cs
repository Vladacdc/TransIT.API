using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Role : IdentityRole, IAuditableEntity, IEntityId<string>
    {
        public Role()
        {

        }
        
        public string TransName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
    }
}
