using System.Threading;
using System.Threading.Tasks;
using BlazorShared.Models.Patient;
using ClinicManagement.Domain.Specifications.Client;
using ClinicManagement.Domain.ValueObjects;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using PluralsightDdd.SharedKernel.Interfaces;
using IMapper = AutoMapper.IMapper;

namespace ClinicManagement.Api.Endpoints.Patient
{
  public class Create : Endpoint<CreatePatientRequest, Results<Ok<CreatePatientResponse>, NotFound>>
  {
    private readonly IRepository<Domain.Aggregates.ClientAggregate.Client> _repository;
    private readonly IMapper _mapper;

    public Create(IRepository<Domain.Aggregates.ClientAggregate.Client> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public override void Configure()
    {
      Post("api/patients");
      AllowAnonymous();
      Description(d =>
          d.WithSummary("Creates a new Patient")
           .WithDescription("Creates a new Patient")
           .WithName("patients.create")
           .WithTags("PatientEndpoints"));
    }

    public override async Task<Results<Ok<CreatePatientResponse>, NotFound>> ExecuteAsync(CreatePatientRequest request, CancellationToken cancellationToken)
    {
      var response = new CreatePatientResponse(request.CorrelationId);

      var spec = new ClientByIdIncludePatientsSpec(request.ClientId);
      var client = await _repository.GetBySpecAsync(spec);
      if (client == null) return TypedResults.NotFound();

      // right now we only add huskies
      var newPatient = new Domain.Aggregates.ClientAggregate.Patient
      {
        ClientId = client.Id,
        Name = request.PatientName,
        AnimalValueObject = new AnimalValueObject("Dog", "Husky")
      };
      client.Patients.Add(newPatient);

      await _repository.UpdateAsync(client);

      var dto = _mapper.Map<PatientDto>(newPatient);
      response.Patient = dto;

      return TypedResults.Ok(response);
    }
  }
}
