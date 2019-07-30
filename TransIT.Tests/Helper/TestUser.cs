using TransIT.BLL.DTOs;

namespace TransIT.Tests
{
    /// <summary>
    /// User DTO for Tests.
    /// </summary>
    public class TestUser : UserDTO
    {
        public TestUser()
        {
            UserName = "testAdmin";
            Password = "HelloWorld123@";
            FirstName = "Ivan";
            MiddleName = "Petro";
            LastName = "Ivanovych";
            Role = new RoleDTO
            {
                Name = "ADMIN",
                TransName = "Адмін"
            };
            Email = "shewchenkoandriy@gmail.com";
        }
    }
}
