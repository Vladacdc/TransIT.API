using System;

namespace TransIT.DAL.Models.Entities.Abstractions
{
    public interface IAuditableEntity
    {
        string CreatedById { get; set; }
        string UpdatedById { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
