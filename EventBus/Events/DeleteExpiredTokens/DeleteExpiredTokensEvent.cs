namespace EventBus.Events.DeleteExpiredTokens;

public record DeleteExpiredTokensEvent(int TokenLifetimeSeconds) : IntegrationEvent
{
}
