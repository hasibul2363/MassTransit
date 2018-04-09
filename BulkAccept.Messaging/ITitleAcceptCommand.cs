using System;

namespace BulkAccept.Messaging
{
    public interface ITitleAcceptCommand
    {
        int RefNo { get; set; }
        string VinNo { get; set; }
        DateTime AcceptedDate { get; set; }
        Guid CorrelationId { get; set; }
    }
}
