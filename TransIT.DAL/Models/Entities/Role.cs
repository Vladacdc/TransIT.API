using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public class Role : IdentityRole, IAuditableEntity
    {
        public string TransName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
