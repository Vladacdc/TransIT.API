using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Tests.Services.ImplementedServices
{
    public class MalfunctionServiceTests : CrudServiceTest<Malfunction>
    {
        protected override void InitializeService()
        {
            var mock = _repository.As<IMalfunctionRepository>();
            _crudService = new MalfunctionService(_unitOfWork.Object, _logger.Object, mock.Object);
        }
    }
}
