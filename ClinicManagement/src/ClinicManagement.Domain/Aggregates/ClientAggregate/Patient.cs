using ClinicManagement.Domain.ValueObjects;
using PluralsightDdd.SharedKernel;

namespace ClinicManagement.Domain.Aggregates.ClientAggregate
{
  public class Patient : BaseEntity<int>
  {
    public int ClientId { get; set; }
    public string Name { get; set; }
    public string Sex { get; set; }
    public AnimalValueObject AnimalValueObject { get; set; }
    public int? PreferredDoctorId { get; set; }

    public override string ToString()
    {
      return Name;
    }
  }
}
