using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Malfunction : IAuditableEntity, IBaseEntity
    {
        public Malfunction()
        {
            Issue = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? MalfunctionSubgroupId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }

        public virtual MalfunctionSubgroup MalfunctionSubgroup { get; set; }
        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<Issue> Issue { get; set; }
    }
}
