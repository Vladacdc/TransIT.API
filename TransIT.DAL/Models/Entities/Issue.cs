using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Issue : IAuditableEntity, IBaseEntity
    {
        public Issue()
        {
            Bill = new HashSet<Bill>();
            IssueLog = new HashSet<IssueLog>();
        }

        public int Id { get; set; }
        public string Summary { get; set; }
        public int? Warranty { get; set; }
        public DateTime? Deadline { get; set; }
        public int? StateId { get; set; }
        public int? AssignedToId { get; set; }
        public int VehicleId { get; set; }
        public int? MalfunctionId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public int? Number { get; set; }
        public int Priority { get; set; }
        public DateTime Date { get; set; }

        public virtual Employee AssignedTo { get; set; }
        public virtual User Create { get; set; }
        public virtual Malfunction Malfunction { get; set; }
        public virtual User Mod { get; set; }
        public virtual State State { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<Bill> Bill { get; set; }
        public virtual ICollection<IssueLog> IssueLog { get; set; }
    }
}
