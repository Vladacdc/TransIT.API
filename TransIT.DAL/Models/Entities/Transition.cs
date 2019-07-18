using System;
using System.Collections.Generic;
using System.Text;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Transition : IAuditableEntity
    {
        public int Id { get; set; }
        public int FromStateId { get; set; }
        public int ToStateId { get; set; }
        public int ActionTypeId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public bool IsFixed { get; set; }

        public virtual ActionType ActionType { get; set; }
        public virtual User Create { get; set; }
        public virtual State FromState { get; set; }
        public virtual User Mod { get; set; }
        public virtual State ToState { get; set; }
    }
}
