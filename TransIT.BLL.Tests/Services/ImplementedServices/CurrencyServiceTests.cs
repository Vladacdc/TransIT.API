using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Tests.Services.ImplementedServices
{
    class CurrencyServiceTests : CrudServiceTest<Currency>
    {
        protected override void InitializeService()
        {
            var mock = _repository.As<ICurrencyRepository>();
            _crudService = new CurrencyService(_unitOfWork.Object, _logger.Object, mock.Object);
        }
    }
}
