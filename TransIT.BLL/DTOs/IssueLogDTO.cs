using System;

namespace TransIT.BLL.DTOs
{
    public class IssueLogDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Expenses { get; set; }
        public StateDTO OldState { get; set; }
        public StateDTO NewState { get; set; }
        public SupplierDTO Supplier { get; set; }
        public ActionTypeDTO ActionType { get; set; }
        public IssueDTO Issue { get; set; }
        public WorkTypeDTO WorkTypeDto { get; set; }
        public UserDTO Create { get; set; }
        public UserDTO Mod { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }

        public DocumentDTO[] Documents { get; set; }
    }
}
