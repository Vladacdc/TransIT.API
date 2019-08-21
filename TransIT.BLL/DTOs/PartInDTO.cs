using System;

namespace TransIT.BLL.DTOs
{
    public class PartInDTO
    {
        private uint amount;
        private float price;

        public PartInDTO()
        {

        }

        public int Id { get; set; }
        public int UnitId { get; set; }
        public int? PartId { get; set; }
        public int CurrencyId { get; set; }

        public string Batch { get; set; }
        public uint Amount
        {
            get => amount;
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("Only positive values are accepted", nameof(Amount));
                amount = value;
            }
        }
        public float Price
        {
            get => price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Only positive values are accepted", nameof(Price));
                }
                price = value;
            }
        }
        public DateTime ArrivalDate { get; set; }
        public CurrencyDTO Currency { get; set; }
        public UnitDTO Unit { get; set; }
        public PartDTO Part { get; set; }
    }
}
