using MeetMe.Shared.Abstractions.Events;
using MeetMe.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace MeetMe.Shared.Messaging;

internal sealed class InMemoryMessageBroker : IMessageBroker
{
    private readonly IAsyncEventDispatcher _asyncEventDispatcher;
    private readonly ILogger<InMemoryMessageBroker> _logger;

    public InMemoryMessageBroker(IAsyncEventDispatcher asyncEventDispatcher, ILogger<InMemoryMessageBroker> logger)
    {
        _asyncEventDispatcher = asyncEventDispatcher;
        _logger = logger;
    }

    public async Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        await _asyncEventDispatcher.PublishAsync(@event, cancellationToken);
    }
}