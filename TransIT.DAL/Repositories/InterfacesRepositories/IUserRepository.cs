using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Repositories.InterfacesRepositories
{
    public interface IUserRepository
    {
        string CurrentUserId { get; set; }
    }
}
