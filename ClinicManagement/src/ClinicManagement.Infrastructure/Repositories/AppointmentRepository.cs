using ClinicManagement.Domain.Aggregates.AppointmentAggregate;
using ClinicManagement.Domain.Interfaces.Repositories;
using ClinicManagement.Infrastructure.Data;
using ClinicManagement.Infrastructure.Repositories.Base;

namespace ClinicManagement.Infrastructure.Repositories;

public class AppointmentRepository(AppDbContext dbContext) : EfRepository<AppointmentType>(dbContext), IAppointmentRepository
{
  //todo: implement custom methods
}
