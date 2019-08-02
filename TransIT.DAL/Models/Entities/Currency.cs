using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public class Currency : IAuditableEntity, IBaseEntity
    {
        public Currency()
        {
            Supplier = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        
        public virtual ICollection<Supplier> Supplier { get; set; }
    }
}
