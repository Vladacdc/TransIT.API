using System;

namespace TransIT.BLL.DTOs
{
    public class BillDTO
    {
        public int Id { get; set; }
        public decimal? Sum { get; set; }
        public DocumentDTO Document { get; set; }
        public IssueDTO Issue { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
