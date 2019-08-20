using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public class Document : IAuditableEntity, IBaseEntity
    {
        public Document()
        {
            Bill = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? IssueLogId { get; set; }
        public string ContentType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public DateTime? NewDate { get; set; }
        public string Path { get; set; }
        public virtual User Create { get; set; }
        public virtual IssueLog IssueLog { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<Bill> Bill { get; set; }
    }
}
