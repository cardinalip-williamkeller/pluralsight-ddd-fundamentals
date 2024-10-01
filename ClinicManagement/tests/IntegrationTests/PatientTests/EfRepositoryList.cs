using ClinicManagement.Domain.Aggregates.ClientAggregate;
using ClinicManagement.Infrastructure.Repositories.Base;

namespace IntegrationTests.PatientTests
{
  public class EfRepositoryList : BaseEfRepoTestFixture
  {
    private readonly EfRepository<Client> _repository;

    public EfRepositoryList()
    {
      _repository = GetRepository<Client>();
    }

    //[Fact]
    //public async Task ListsPatientAfterAddingIt()
    //{
    //  await AddPatient();

    //  var patients = (await _repository.ListAsync<FrontDesk.Domain.Aggregates.Patient, int>()).ToList();

    //  Assert.True(patients?.Count > 0);
    //}

    //private async Task<FrontDesk.Domain.Aggregates.Patient> AddPatient()
    //{
    //  var patient = new PatientBuilder().Id(7).Build();

    //  await _repository.AddAsync<FrontDesk.Domain.Aggregates.Patient, int>(patient);

    //  return patient;
    //}
  }
}
