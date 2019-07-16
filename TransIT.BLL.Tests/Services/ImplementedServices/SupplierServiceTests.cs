using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Tests.Services.ImplementedServices
{
    public class SupplierServiceTests : CrudServiceTest<Supplier>
    {
        protected override void InitializeService()
        {
            var mock = _repository.As<ISupplierRepository>();
            _crudService = new SupplierService(_unitOfWork.Object, _logger.Object, mock.Object);
        }
    }
}
