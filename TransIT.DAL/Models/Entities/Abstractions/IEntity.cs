using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.DAL.Models.Entities.Abstractions
{
    public interface IAuditableEntity
    {
        string CreatedById { get; set; }
        string ModifiedById { get; set; }
    }
}
