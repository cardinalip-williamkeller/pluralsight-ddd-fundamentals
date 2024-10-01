using System.Threading.Tasks;

namespace ClinicManagement.Domain.Interfaces
{
  public interface IFileSystem
  {
    Task<bool> SavePicture(string pictureName, string pictureBase64);
  }
}
