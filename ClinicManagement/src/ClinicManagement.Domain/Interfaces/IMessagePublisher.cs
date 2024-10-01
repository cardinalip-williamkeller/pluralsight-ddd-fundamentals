using System.Threading.Tasks;

namespace ClinicManagement.Domain.Interfaces
{
  public interface IMessagePublisher
  {
    Task Publish(object applicationEvent);
  }
}
