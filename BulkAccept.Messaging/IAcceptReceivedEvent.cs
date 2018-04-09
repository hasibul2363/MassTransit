using System;

namespace BulkAccept.Messaging
{
    public interface IAcceptReceivedEvent
    {
        Guid CorrelationId { get; }
        int RefNo { get; }
        string VinNo { get; }
    }
}