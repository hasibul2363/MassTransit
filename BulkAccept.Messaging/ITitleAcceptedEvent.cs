using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkAccept.Messaging
{
    public interface ITitleAcceptedEvent
    {
        int RefNo { get;  }
        string VinNo { get;  }
        DateTime AcceptedDate { get;  }
        Guid CorrelationId { get;  }
    }
    public interface ITitleFailedEvent
    {
        int RefNo { get; }
        string VinNo { get; }
        DateTime AcceptedDate { get; }
        string Reason { get; }
        Guid CorrelationId { get; }
    }
    
}
