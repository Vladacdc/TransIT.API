using System;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public class Part : IAuditableEntity, IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        //TODO

        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
