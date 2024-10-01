using ClinicManagement.Domain.Aggregates.ClientAggregate;
using ClinicManagement.Infrastructure.Repositories.Base;

namespace IntegrationTests.PatientTests
{
  public class EfRepositoryGetById : BaseEfRepoTestFixture
  {
    private readonly EfRepository<Client> _repository;

    public EfRepositoryGetById()
    {
      _repository = GetRepository<Client>();
    }

    //[Fact]
    //public async Task GetsByIdPatientAfterAddingIt()
    //{
    //  var id = 9;
    //  var patient = await AddPatient(id);

    //  var newPatient = await _repository.GetByIdAsync<FrontDesk.Domain.Aggregates.Patient, int>(id);

    //  Assert.Equal(patient, newPatient);
    //  Assert.True(newPatient?.Id == id);
    //}

    //private async Task<FrontDesk.Domain.Aggregates.Patient> AddPatient(int id)
    //{
    //  var patient = new PatientBuilder().Id(id).Build();

    //  await _repository.AddAsync<FrontDesk.Domain.Aggregates.Patient, int>(patient);

    //  return patient;
    //}
  }
}
