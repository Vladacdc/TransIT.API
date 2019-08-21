using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public class Part : IAuditableEntity, IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int UnitId { get; set; }
        public int ManufacturerId { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Unit Unit { get; set; }
        public Manufacturer Manufacturer{ get; set; }
        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }

        public ICollection<IssueLogParts> IssueLogParts { get; set; }
        public ICollection<SuppliersParts> SuppliersParts { get; set; }
    }
}
