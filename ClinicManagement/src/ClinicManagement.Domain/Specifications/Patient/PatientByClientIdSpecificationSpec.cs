using Ardalis.Specification;

namespace ClinicManagement.Domain.Specifications.Patient
{
  public class PatientByClientIdSpecificationSpec : Specification<Aggregates.ClientAggregate.Patient>
  {
    public PatientByClientIdSpecificationSpec(int clientId)
    {
      Query
          .Where(patient => patient.ClientId == clientId);

      Query.OrderBy(patient => patient.Name);
    }
  }
}
