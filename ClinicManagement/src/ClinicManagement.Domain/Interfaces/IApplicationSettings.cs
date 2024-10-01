using System;

namespace ClinicManagement.Domain.Interfaces
{
  public interface IApplicationSettings
  {
    int ClinicId { get; }
    DateTime TestDate { get; }
  }
}
