using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorShared.Models.Doctor;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using PluralsightDdd.SharedKernel.Interfaces;
using IMapper = AutoMapper.IMapper;

namespace ClinicManagement.Api.Endpoints.Doctor
{
  public class List : Endpoint<ListDoctorRequest, Results<Ok<ListDoctorResponse>, NotFound>>
  {
    private readonly IRepository<Domain.Aggregates.DoctorAggregate.Doctor> _repository;
    private readonly IMapper _mapper;

    public List(IRepository<Domain.Aggregates.DoctorAggregate.Doctor> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public override void Configure()
    {
      Get("api/doctors");
      AllowAnonymous();
      Description(d =>
          d.WithSummary("List Doctors")
           .WithDescription("List Doctors")
           .WithName("doctors.List")
           .WithTags("DoctorEndpoints"));
    }

    public override async Task<Results<Ok<ListDoctorResponse>, NotFound>> ExecuteAsync(ListDoctorRequest request, CancellationToken cancellationToken)
    {
      var response = new ListDoctorResponse(request.CorrelationId);

      var doctors = await _repository.ListAsync();
      if (doctors is null) return TypedResults.NotFound();

      response.Doctors = _mapper.Map<List<DoctorDto>>(doctors);
      response.Count = response.Doctors.Count;

      return TypedResults.Ok(response);
    }
  }
}
