using System.Threading;
using System.Threading.Tasks;
using BlazorShared.Models.Client;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PluralsightDdd.SharedKernel.Interfaces;
using IMapper = AutoMapper.IMapper;

namespace ClinicManagement.Api.Endpoints.Client
{
  public class Create : Endpoint<CreateClientRequest, CreateClientResponse>
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
      Post("api/clients");
      AllowAnonymous();
      Description(d =>
          d.WithSummary("Creates a new Client")
           .WithDescription("Creates a new Client")
           .WithName("clients.create")
           .WithTags("ClientEndpoints"));
    }

    public override async Task<CreateClientResponse> ExecuteAsync(CreateClientRequest request, CancellationToken cancellationToken)
    {
      var response = new CreateClientResponse(request.CorrelationId);

      var toAdd = _mapper.Map<Domain.Aggregates.ClientAggregate.Client>(request);
      toAdd = await _repository.AddAsync(toAdd);

      var dto = _mapper.Map<ClientDto>(toAdd);
      response.Client = dto;

      return response;
    }
  }
}
