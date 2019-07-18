using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class IssueLog : IAuditableEntity
    {
        public IssueLog()
        {
            Document = new HashSet<Document>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Expenses { get; set; }
        public int? OldStateId { get; set; }
        public int? NewStateId { get; set; }
        public int? SupplierId { get; set; }
        public int? ActionTypeId { get; set; }
        public int? IssueId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreatedById { get; set; }
        public int? ModifiedById { get; set; }

        public virtual ActionType ActionType { get; set; }
        public virtual User Create { get; set; }
        public virtual Issue Issue { get; set; }
        public virtual User Mod { get; set; }
        public virtual State NewState { get; set; }
        public virtual State OldState { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Document> Document { get; set; }
    }
}
