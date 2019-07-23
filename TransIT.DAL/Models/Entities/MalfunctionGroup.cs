using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class MalfunctionGroup : IAuditableEntity, IEntityId<int>
    {
        public MalfunctionGroup()
        {
            MalfunctionSubgroup = new HashSet<MalfunctionSubgroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<MalfunctionSubgroup> MalfunctionSubgroup { get; set; }
    }
}
