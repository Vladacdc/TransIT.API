using TransIT.DAL.Models;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TransITDBContext _dbContext;

        public UserRepository(TransITDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string CurrentUserId
        {
            get => _dbContext.CurrentUserId;
            set => _dbContext.CurrentUserId = value;
        }
    }
}
