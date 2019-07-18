﻿using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Vehicle : IAuditableEntity
    {
        public Vehicle()
        {
            Issue = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public int? VehicleTypeId { get; set; }
        public string Vincode { get; set; }
        public string InventoryId { get; set; }
        public string RegNum { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public DateTime? CommissioningDate { get; set; }
        public int? LocationId { get; set; }

        public virtual User Create { get; set; }
        public virtual Location Location { get; set; }
        public virtual User Mod { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public virtual ICollection<Issue> Issue { get; set; }
    }
}
