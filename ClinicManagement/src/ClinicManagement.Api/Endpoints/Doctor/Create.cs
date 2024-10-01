using System.Threading;
using System.Threading.Tasks;
using BlazorShared.Models.Doctor;
using ClinicManagement.Domain.IntegrationEvents;
using ClinicManagement.Domain.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PluralsightDdd.SharedKernel.Interfaces;
using IMapper = AutoMapper.IMapper;

namespace ClinicManagement.Api.Endpoints.Doctor
{
  public class Create : Endpoint<CreateDoctorRequest, CreateDoctorResponse>
  {
    private readonly IRepository<Domain.Aggregates.DoctorAggregate.Doctor> _repository;
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _messagePublisher;
    private readonly ILogger<Create> _logger;

    public Create(IRepository<Domain.Aggregates.DoctorAggregate.Doctor> repository,
      IMapper mapper,
      IMessagePublisher messagePublisher,
      ILogger<Create> logger)
    {
      _repository = repository;
      _mapper = mapper;
      _messagePublisher = messagePublisher;
      _logger = logger;
    }

    public override void Configure()
    {
      Post("api/doctors");
      AllowAnonymous();
      Description(d =>
          d.WithSummary("Creates a new Doctor")
           .WithDescription("Creates a new Doctor")
           .WithName("doctors.create")
           .WithTags("DoctorEndpoints"));
    }

    public override async Task<CreateDoctorResponse> ExecuteAsync(CreateDoctorRequest request, CancellationToken cancellationToken)
    {
      var response = new CreateDoctorResponse(request.CorrelationId);

      var toAdd = _mapper.Map<Domain.Aggregates.DoctorAggregate.Doctor>(request);
      toAdd = await _repository.AddAsync(toAdd);

      var dto = _mapper.Map<DoctorDto>(toAdd);
      response.Doctor = dto;

      // Note: These messages could be triggered from the Repository or DbContext events
      // In the DbContext you could look for entities marked with an interface saying they needed
      // to be synchronized via cross-domain events and publish the appropriate message.
      var appEvent = new DoctorCreatedIntegrationEvent(toAdd.Id, toAdd.Name);

      _logger.LogInformation("Sending doctor created event: {0}", appEvent);
      await _messagePublisher.Publish(appEvent);

      return response;
    }
  }
}
