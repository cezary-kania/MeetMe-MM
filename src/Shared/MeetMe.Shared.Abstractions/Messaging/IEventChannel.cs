using System.Threading.Channels;
using MeetMe.Shared.Abstractions.Events;

namespace MeetMe.Shared.Abstractions.Messaging;

public interface IEventChannel
{
    ChannelReader<IEvent> Reader { get; }
    ChannelWriter<IEvent> Writer { get; }
}