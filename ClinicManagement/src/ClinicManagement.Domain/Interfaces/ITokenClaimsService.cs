using System.Threading.Tasks;

namespace ClinicManagement.Domain.Interfaces
{
  public interface ITokenClaimsService
  {
    Task<string> GetTokenAsync(string userName);
  }
}
