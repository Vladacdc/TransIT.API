using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class State : IAuditableEntity, IEntityId<int>
    {
        public State()
        {
            Issue = new HashSet<Issue>();
            IssueLogNewState = new HashSet<IssueLog>();
            IssueLogOldState = new HashSet<IssueLog>();
            TransitionFromState = new HashSet<Transition>();
            TransitionToState = new HashSet<Transition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TransName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public bool IsFixed { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<Issue> Issue { get; set; }
        public virtual ICollection<IssueLog> IssueLogNewState { get; set; }
        public virtual ICollection<IssueLog> IssueLogOldState { get; set; }
        public virtual ICollection<Transition> TransitionFromState { get; set; }
        public virtual ICollection<Transition> TransitionToState { get; set; }
    }
}
