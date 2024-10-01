using System.Threading;
using System.Threading.Tasks;
using BlazorShared.Models.Room;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PluralsightDdd.SharedKernel.Interfaces;
using IMapper = AutoMapper.IMapper;

namespace ClinicManagement.Api.Endpoints.Room
{
  public class Create : Endpoint<CreateRoomRequest, CreateRoomResponse>
  {
    private readonly IRepository<Domain.Aggregates.RoomAggregate.Room> _repository;
    private readonly IMapper _mapper;

    public Create(IRepository<Domain.Aggregates.RoomAggregate.Room> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public override void Configure()
    {
      Post("api/rooms");
      AllowAnonymous();
      Description(d =>
          d.WithSummary("Creates a new Room")
           .WithDescription("Creates a new Room")
           .WithName("rooms.create")
           .WithTags("RoomEndpoints"));
    }

    public override async Task<CreateRoomResponse> ExecuteAsync(CreateRoomRequest request, CancellationToken cancellationToken)
    {
      var response = new CreateRoomResponse(request.CorrelationId);

      var toAdd = _mapper.Map<Domain.Aggregates.RoomAggregate.Room>(request);
      toAdd = await _repository.AddAsync(toAdd);

      var dto = _mapper.Map<RoomDto>(toAdd);
      response.Room = dto;

      return response;
    }
  }
}
