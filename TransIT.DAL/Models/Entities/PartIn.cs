using System;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public class PartIn : IAuditableEntity, IBaseEntity
    {
        public PartIn()
        {

        }

        public int Id { get; set; }
        public uint Amount { get; set; }
        public float Price { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Batch { get; set; }
        public int? UnitId { get; set; }
        public int? PartId { get; set; }
        public int CurrencyId { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Currency Currency { get; set; }
    }
}
