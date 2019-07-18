using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Location: IAuditableEntity
    {
        public Location()
        {
            Vehicle = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}
