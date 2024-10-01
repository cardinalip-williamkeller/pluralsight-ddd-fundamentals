using System.Threading.Tasks;

namespace ClinicManagement.Domain.Interfaces
{
  public interface IEmailSender
  {
    Task SendEmailAsync(string to, string from, string subject, string body);
  }
}
