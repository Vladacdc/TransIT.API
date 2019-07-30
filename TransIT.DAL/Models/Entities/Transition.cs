﻿using System;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public class Transition : IAuditableEntity, IBaseEntity
    {
        public int Id { get; set; }
        public int FromStateId { get; set; }
        public int ToStateId { get; set; }
        public int ActionTypeId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public bool IsFixed { get; set; }

        public virtual ActionType ActionType { get; set; }
        public virtual User Create { get; set; }
        public virtual State FromState { get; set; }
        public virtual User Mod { get; set; }
        public virtual State ToState { get; set; }
    }
}
