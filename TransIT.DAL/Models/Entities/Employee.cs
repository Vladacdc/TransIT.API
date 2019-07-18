using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Employee : IAuditableEntity
    {
        public Employee()
        {
            Issue = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public int PostId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public int BoardNumber { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Issue> Issue { get; set; }
    }
}
