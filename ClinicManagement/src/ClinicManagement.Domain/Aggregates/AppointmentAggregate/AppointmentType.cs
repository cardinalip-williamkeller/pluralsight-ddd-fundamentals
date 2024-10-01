using System.ComponentModel.DataAnnotations.Schema;
//using ClinicManagement.JustAnotherProject;
using PluralsightDdd.SharedKernel;
using PluralsightDdd.SharedKernel.Interfaces;

namespace ClinicManagement.Domain.Aggregates.AppointmentAggregate
{
  public class AppointmentType : BaseEntity<int>, IAggregateRoot
  {
    public string Name { get; private set; }
    public string Code { get; private set; }
    public int Duration { get; private set; }
    //[NotMapped] public Example Example { get; set; }

    public AppointmentType(int id, string name, string code, int duration)
    {
      Id = id;
      Name = name;
      Code = code;
      Duration = duration;
    }

    public override string ToString()
    {
      return Name;
    }
  }
}
