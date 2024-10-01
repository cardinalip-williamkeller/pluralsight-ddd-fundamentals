using ClinicManagement.Domain.Aggregates.ClientAggregate;
using ClinicManagement.Infrastructure.Repositories.Base;

namespace IntegrationTests.PatientTests
{
  public class EfRepositoryUpdate : BaseEfRepoTestFixture
  {
    private readonly EfRepository<Client> _repository;

    public EfRepositoryUpdate()
    {
      _repository = GetRepository<Client>();
    }

    //[Fact]
    //public async Task UpdatesPatientAfterAddingIt()
    //{
    //  var id = 2;
    //  var name = "changed";

    //  var patient = await AddPatient(id);

    //  patient.UpdateName(name);
    //  await _repository.UpdateAsync<FrontDesk.Domain.Aggregates.Patient, int>(patient);

    //  var updatedPatient = await _repository.GetByIdAsync<FrontDesk.Domain.Aggregates.Patient, int>(id);

    //  Assert.Equal(name, updatedPatient.Name);
    //}

    //private async Task<FrontDesk.Domain.Aggregates.Patient> AddPatient(int id)
    //{
    //  var patient = new PatientBuilder().Id(id).Build();

    //  await _repository.AddAsync<FrontDesk.Domain.Aggregates.Patient, int>(patient);

    //  return patient;
    //}
  }
}
