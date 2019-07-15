namespace TransIT.BLL.DTOs
{
    public class EmployeeDTO
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public int BoardNumber { get; set; }
        public PostDTO Post { get; set; }
    }
}
