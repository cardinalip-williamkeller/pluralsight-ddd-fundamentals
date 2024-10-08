﻿using System.Threading.Tasks;
using ClinicManagement.Domain.Aggregates.RoomAggregate;
using ClinicManagement.Infrastructure.Repositories.Base;
using UnitTests.Builders;
using Xunit;

namespace IntegrationTests.RoomTests
{
  public class EfRepositoryDelete : BaseEfRepoTestFixture
  {
    private readonly EfRepository<Room> _repository;

    public EfRepositoryDelete()
    {
      _repository = GetRepository<Room>();
    }

    [Fact]
    public async Task DeletesRoomAfterAddingIt()
    {
      var id = 8;

      var room = await AddRoom(id);
      await _repository.DeleteAsync(room);

      Assert.DoesNotContain(await _repository.ListAsync(),
          i => i.Id == id);
    }

    private async Task<Room> AddRoom(int id)
    {
      var room = new RoomBuilder().Id(id).Build();

      await _repository.AddAsync(room);

      return room;
    }
  }
}
