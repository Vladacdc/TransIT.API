using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Currency : IAuditableEntity
    {
        public Currency()
        {
            Supplier = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreatedById { get; set; }
        public int? ModifiedById { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        
        public virtual ICollection<Supplier> Supplier { get; set; }
    }
}
