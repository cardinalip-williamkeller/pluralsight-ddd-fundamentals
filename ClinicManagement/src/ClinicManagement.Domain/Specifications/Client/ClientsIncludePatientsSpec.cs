using Ardalis.Specification;

namespace ClinicManagement.Domain.Specifications.Client
{
  public class ClientsIncludePatientsSpec : Specification<Aggregates.ClientAggregate.Client>
  {
    public ClientsIncludePatientsSpec()
    {
      Query
        .Include(client => client.Patients)
        .OrderBy(client => client.FullName);
    }
  }
}
