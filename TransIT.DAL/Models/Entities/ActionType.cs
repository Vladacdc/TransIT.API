using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class ActionType : IAuditableEntity, IBaseEntity
    {
        public ActionType()
        {
            IssueLog = new HashSet<IssueLog>();
            Transition = new HashSet<Transition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public bool IsFixed { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<IssueLog> IssueLog { get; set; }
        public virtual ICollection<Transition> Transition { get; set; }
    }
}
