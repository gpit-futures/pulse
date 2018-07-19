using System.Collections.Generic;

namespace Pulse.Infrastructure.MessageQueue
{
    public interface IMessage
    {
        string Source { get; set; }

        string Destination { get; set; }

        Dictionary<string, dynamic> Body { get; set; }
    }
}