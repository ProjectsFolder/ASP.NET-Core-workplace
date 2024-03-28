using EventBus.Events.DeleteExpiredTokens;
using EventBus.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace EventBus;

public static class Dependency
{
    public static IServiceCollection AddIntegrationEvents(this IServiceCollection services)
    {
        services.AddSubscription<DeleteExpiredTokensEvent, DeleteExpiredTokensHandler>();

        return services;
    }

    private static void AddSubscription<TEvent, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>
        (this IServiceCollection services)
        where TEvent : Events.IntegrationEvent
        where THandler : class, IIntegrationEventHandler<TEvent>
    {
        services.AddKeyedTransient<IIntegrationEventHandler, THandler>(typeof(TEvent));
        services.Configure<Subscriptions>(o =>
        {
            o.EventTypes[typeof(TEvent).Name] = typeof(TEvent);
        });
    }
}
