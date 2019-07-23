using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.DAL.Models.Entities.Abstractions
{
    public interface IEntityId<TId>
    {
        TId Id { get; set; }
    }
}
