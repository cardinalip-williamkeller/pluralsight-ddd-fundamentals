using System;

namespace ClinicManagement.Domain.IntegrationEvents;

public record DoctorCreatedIntegrationEvent(int Id, string Name)
{
  public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
}
