using PluralsightDdd.SharedKernel;
using PluralsightDdd.SharedKernel.Interfaces;

namespace ClinicManagement.Domain.Aggregates.RoomAggregate
{
  public class Room : BaseEntity<int>, IAggregateRoot
  {
    public string Name { get; set; }

    private Room() { }

    public Room(int id, string name)
    {
      Id = id;
      Name = name;
    }

    public override string ToString()
    {
      return Name;
    }
  }
}
