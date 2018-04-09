using System;

namespace BulkAccept.Messaging
{
    public interface IAcceptProcessedEvent
    {
        Guid CorrelationId { get; set; }
        int RefNo { get; set; }
    }
}