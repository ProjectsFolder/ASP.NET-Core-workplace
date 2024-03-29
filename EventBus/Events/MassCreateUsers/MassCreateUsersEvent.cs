using EventBus.Events.MassCreateUsers.Dto;

namespace EventBus.Events.MassCreateUsers;

public record MassCreateUsersEvent(IEnumerable<UserDto> Users) : IntegrationEvent
{
}
