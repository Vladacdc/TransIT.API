namespace TransIT.BLL.DTOs
{
    public class EmployeeDTO
    {
        /// <summary>
        /// Using a null object pattern instead of null.
        /// </summary>
        public static readonly EmployeeDTO DoesNotExist = new EmployeeDTO() { Id = -1 };

        /// <summary>
        /// Indicates that we cannot attach a user to employee.
        /// </summary>
        public static readonly EmployeeDTO CannotAttachUser = new EmployeeDTO { Id = -1 };

        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public int BoardNumber { get; set; }
        public PostDTO Post { get; set; }
        public UserDTO AttachedUser { get; set; }
    }
}
