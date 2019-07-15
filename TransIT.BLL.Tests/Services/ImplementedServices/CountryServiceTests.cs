using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.BLL.Tests.Services.ImplementedServices
{
    class CountryServiceTests : CrudServiceTest<Country>
    {
        protected override void InitializeService()
        {
            var mock = _repository.As<ICountryRepository>();
            _crudService = new CountryService(_unitOfWork.Object, _logger.Object, mock.Object);
        }
    }
}
