using System;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Bill : IAuditableEntity, IBaseEntity
    {
        public int Id { get; set; }
        public decimal? Sum { get; set; }
        public int? DocumentId { get; set; }
        public int? IssueId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }

        public virtual User Create { get; set; }
        public virtual Document Document { get; set; }
        public virtual Issue Issue { get; set; }
        public virtual User Mod { get; set; }
    }
}
