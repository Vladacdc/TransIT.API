using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class MalfunctionSubgroup : IAuditableEntity, IEntityId<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MalfunctionGroupId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }

        public virtual User Create { get; set; }
        public virtual MalfunctionGroup MalfunctionGroup { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<Malfunction> Malfunction { get; set; }
    }
}
