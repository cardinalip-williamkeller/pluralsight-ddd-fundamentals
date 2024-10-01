using ClinicManagement.Domain.Aggregates.ClientAggregate;
using ClinicManagement.Infrastructure.Repositories.Base;

namespace IntegrationTests.PatientTests
{
  public class EfRepositoryDelete : BaseEfRepoTestFixture
  {
    private readonly EfRepository<Client> _repository;

    public EfRepositoryDelete()
    {
      _repository = GetRepository<Client>();
    }

    //[Fact]
    //public async Task DeletesPatientAfterAddingIt()
    //{
    //  var id = 8;

    //  var patient = await AddPatient(id);
    //  await _repository.DeleteAsync<FrontDesk.Domain.Aggregates.Patient, int>(patient);

    //  Assert.DoesNotContain(await _repository.ListAsync<FrontDesk.Domain.Aggregates.Patient, int>(),
    //      i => i.Id == id);
    //}

    //private async Task<FrontDesk.Domain.Aggregates.Patient> AddPatient(int id)
    //{
    //  var patient = new PatientBuilder().Id(id).Build();

    //  await _repository.AddAsync<FrontDesk.Domain.Aggregates.Patient, int>(patient);

    //  return patient;
    //}
  }
}
