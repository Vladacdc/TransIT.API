using System;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Token : IAuditableEntity, IEntityId<int>
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
    }
}
