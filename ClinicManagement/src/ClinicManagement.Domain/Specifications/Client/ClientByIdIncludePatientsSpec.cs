using Ardalis.Specification;

namespace ClinicManagement.Domain.Specifications.Client
{
  public class ClientByIdIncludePatientsSpec : Specification<Aggregates.ClientAggregate.Client>, ISingleResultSpecification
  {
    public ClientByIdIncludePatientsSpec(int clientId)
    {
      Query
        .Include(client => client.Patients)
        .Where(client => client.Id == clientId);
    }
  }
}
