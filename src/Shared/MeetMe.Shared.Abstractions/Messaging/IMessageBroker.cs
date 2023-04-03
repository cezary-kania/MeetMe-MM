using MeetMe.Shared.Abstractions.Events;

namespace MeetMe.Shared.Abstractions.Messaging;

public interface IMessageBroker
{
    Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
}