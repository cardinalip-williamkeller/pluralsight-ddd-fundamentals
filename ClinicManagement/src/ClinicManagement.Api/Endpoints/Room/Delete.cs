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
  public class Delete : Endpoint<DeleteRoomRequest, DeleteRoomResponse>
  {
    private readonly IRepository<Domain.Aggregates.RoomAggregate.Room> _repository;
    private readonly IMapper _mapper;

    public Delete(IRepository<Domain.Aggregates.RoomAggregate.Room> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public override void Configure()
    {
      Delete("api/rooms/{id}");
      AllowAnonymous();
      Description(d =>
          d.WithSummary("Deletes a Room")
           .WithDescription("Deletes a Room")
           .WithName("rooms.delete")
           .WithTags("RoomEndpoints"));
    }

    public override async Task<DeleteRoomResponse> ExecuteAsync(DeleteRoomRequest request, CancellationToken cancellationToken)
    {
      var response = new DeleteRoomResponse(request.CorrelationId);

      var toDelete = _mapper.Map<Domain.Aggregates.RoomAggregate.Room>(request);
      await _repository.DeleteAsync(toDelete);

      return response;
    }
  }
}
