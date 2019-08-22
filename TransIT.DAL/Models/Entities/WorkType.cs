using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public class WorkType : IAuditableEntity, IBaseEntity
    {
        public WorkType()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double EstimatedTime { get; set; }
        public double EstimatedCost { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }

        public virtual ICollection<IssueLog> IssueLog { get; set; }
    }
}
