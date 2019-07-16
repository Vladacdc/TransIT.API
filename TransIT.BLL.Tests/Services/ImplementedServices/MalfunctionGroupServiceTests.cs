using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Tests.Services.ImplementedServices
{
    public class MalfunctionGroupServiceTests : CrudServiceTest<MalfunctionGroup>
    {
        protected override void InitializeService()
        {
            var mock = _repository.As<IMalfunctionGroupRepository>();
            _crudService = new MalfunctionGroupService(_unitOfWork.Object, _logger.Object, mock.Object);
        }
    }
}
