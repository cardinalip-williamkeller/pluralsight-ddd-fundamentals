using Ardalis.Specification;

namespace ClinicManagement.Domain.Specifications.Room
{
  public class RoomSpec : Specification<Aggregates.RoomAggregate.Room>
  {
    public RoomSpec()
    {
      Query.OrderBy(room => room.Name);
    }
  }
}
