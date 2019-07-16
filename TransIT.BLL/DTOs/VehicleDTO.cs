using System;

namespace TransIT.BLL.DTOs
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public VehicleTypeDTO VehicleType { get; set; }
        public string Vincode { get; set; }
        public string InventoryId { get; set; }
        public string RegNum { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public DateTime? CommissioningDate { get; set; }
        public LocationDTO Location { get; set; }
    }
}
