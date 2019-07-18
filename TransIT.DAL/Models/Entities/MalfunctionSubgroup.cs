﻿using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class MalfunctionSubgroup : IAuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MalfunctionGroupId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreatedById { get; set; }
        public int? ModifiedById { get; set; }

        public virtual User Create { get; set; }
        public virtual MalfunctionGroup MalfunctionGroup { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<Malfunction> Malfunction { get; set; }
    }
}
