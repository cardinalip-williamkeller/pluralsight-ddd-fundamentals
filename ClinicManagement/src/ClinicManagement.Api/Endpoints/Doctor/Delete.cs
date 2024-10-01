using System.Threading;
using System.Threading.Tasks;
using BlazorShared.Models.Doctor;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PluralsightDdd.SharedKernel.Interfaces;
using IMapper = AutoMapper.IMapper;

namespace ClinicManagement.Api.Endpoints.Doctor
{
  public class Delete : Endpoint<DeleteDoctorRequest, DeleteDoctorResponse>
  {
    private readonly IRepository<Domain.Aggregates.DoctorAggregate.Doctor> _repository;
    private readonly IMapper _mapper;

    public Delete(IRepository<Domain.Aggregates.DoctorAggregate.Doctor> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public override void Configure()
    {
      Delete("api/doctors/{id}");
      AllowAnonymous();
      Description(d =>
          d.WithSummary("Deletes a Doctor")
           .WithDescription("Deletes a Doctor")
           .WithName("doctors.delete")
           .WithTags("DoctorEndpoints"));
    }

    public override async Task<DeleteDoctorResponse> ExecuteAsync(DeleteDoctorRequest request, CancellationToken cancellationToken)
    {
      var response = new DeleteDoctorResponse(request.CorrelationId);

      var toDelete = new Domain.Aggregates.DoctorAggregate.Doctor(request.Id, "to delete");
      await _repository.DeleteAsync(toDelete);

      return response;
    }
  }
}
