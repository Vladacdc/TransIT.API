using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Post : IAuditableEntity
    {
        public Post()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreatedById { get; set; }
        public int? ModifiedById { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
