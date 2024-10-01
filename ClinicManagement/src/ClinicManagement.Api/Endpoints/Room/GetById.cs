using System.Threading;
using System.Threading.Tasks;
using BlazorShared.Models.Room;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using PluralsightDdd.SharedKernel.Interfaces;
using IMapper = AutoMapper.IMapper;

namespace ClinicManagement.Api.Endpoints.Room
{
  public class GetById : Endpoint<GetByIdRoomRequest, Results<Ok<GetByIdRoomResponse>, NotFound>>
  {
    private readonly IRepository<Domain.Aggregates.RoomAggregate.Room> _repository;
    private readonly IMapper _mapper;

    public GetById(IRepository<Domain.Aggregates.RoomAggregate.Room> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public override void Configure()
    {
      Get("api/rooms/{RoomId}");
      AllowAnonymous();
      Description(d =>
          d.WithSummary("Get a Room by Id")
           .WithDescription("Gets a Room by Id")
           .WithName("rooms.GetById")
           .WithTags("RoomEndpoints"));
    }

    public override async Task<Results<Ok<GetByIdRoomResponse>, NotFound>> ExecuteAsync(GetByIdRoomRequest request, CancellationToken cancellationToken)
    {
      var response = new GetByIdRoomResponse(request.CorrelationId);

      var room = await _repository.GetByIdAsync(request.RoomId);
      if (room is null) return TypedResults.NotFound();

      response.Room = _mapper.Map<RoomDto>(room);

      return TypedResults.Ok(response);
    }
  }
}
