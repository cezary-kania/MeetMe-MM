using MeetMe.Shared.Abstractions.Events;

namespace MeetMe.Shared.Abstractions.Messaging;

public interface IAsyncEventDispatcher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, IEvent;
}