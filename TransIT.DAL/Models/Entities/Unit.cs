using System;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public class Unit : IBaseEntity, IAuditableEntity
    {
        public int Id { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }

        public string CreatedById { get; set; }

        public virtual User Create { get; set; }

        public string UpdatedById { get; set; }

        public virtual User Mod { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}