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
  public class Update : Endpoint<UpdateDoctorRequest, UpdateDoctorResponse>
  {
    private readonly IRepository<Domain.Aggregates.DoctorAggregate.Doctor> _repository;
    private readonly IMapper _mapper;

    public Update(IRepository<Domain.Aggregates.DoctorAggregate.Doctor> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public override void Configure()
    {
      Put("api/doctors");
      AllowAnonymous();
      Description(d =>
          d.WithSummary("Updates a Doctor")
           .WithDescription("Updates a Doctor")
           .WithName("doctors.update")
           .WithTags("DoctorEndpoints"));
    }

    public override async Task<UpdateDoctorResponse> ExecuteAsync(UpdateDoctorRequest request, CancellationToken cancellationToken)
    {
      var response = new UpdateDoctorResponse(request.CorrelationId);

      var toUpdate = _mapper.Map<Domain.Aggregates.DoctorAggregate.Doctor>(request);
      await _repository.UpdateAsync(toUpdate);

      var dto = _mapper.Map<DoctorDto>(toUpdate);
      response.Doctor = dto;

      return response;
    }
  }
}
