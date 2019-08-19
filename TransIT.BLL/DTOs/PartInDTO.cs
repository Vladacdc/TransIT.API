using System;

namespace TransIT.BLL.DTOs
{
    public class PartInDTO
    {
        public PartInDTO()
        {

        }

        public int Id { get; set; }
        public uint Amount { get; set; }
        public float Price { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Batch { get; set; }
        public int? UnitId { get; set; }
        public int? PartId { get; set; }
        public int CurrencyId { get; set; }
        public CurrencyDTO Currency { get; set; }
    }
}
