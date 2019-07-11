using System;

namespace TransIT.BLL.DTOs
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public UserDTO Create { get; set; }
        public UserDTO Mod { get; set; }
        public string FullName { get; set; }
        public CountryDTO Country { get; set; }
        public CurrencyDTO Currency { get; set; }
        public string Edrpou { get; set; }
    }
}
