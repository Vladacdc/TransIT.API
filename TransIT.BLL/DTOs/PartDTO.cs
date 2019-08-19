using System;

namespace TransIT.BLL.DTOs
{
    public class PartDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
