using System;
using System.ComponentModel.DataAnnotations;

namespace TransIT.BLL.DTOs
{
    public class PartInDTO
    {
        public PartInDTO()
        {

        }

        public int Id { get; set; }
        public int UnitId { get; set; }
        public int? PartId { get; set; }
        public int CurrencyId { get; set; }

        public string Batch { get; set; }

        [Range(1, int.MaxValue)]
        public uint Amount { get; set; }

        [Range(0.0000001, double.MaxValue)]
        public float Price { get; set; }
        public DateTime ArrivalDate { get; set; }
        public CurrencyDTO Currency { get; set; }
        public UnitDTO Unit { get; set; }
        public PartDTO Part { get; set; }
    }
}
