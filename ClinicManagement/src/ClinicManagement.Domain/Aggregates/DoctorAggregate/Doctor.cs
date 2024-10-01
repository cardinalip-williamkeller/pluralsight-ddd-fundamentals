//using ClinicManagement.Domain.Aggregates.ClientAggregate;
using PluralsightDdd.SharedKernel;
using PluralsightDdd.SharedKernel.Interfaces;

namespace ClinicManagement.Domain.Aggregates.DoctorAggregate
{
  public class Doctor : BaseEntity<int>, IAggregateRoot
  {
    public string Name { get; set; }

    public Doctor(int id, string name)
    {
      Id = id;
      Name = name;
    }

    public override string ToString()
    {
      //var suchAnNotAllowedPatient = new Patient();

      return Name;
    }
  }
}
