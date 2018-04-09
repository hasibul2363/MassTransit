using BulkAccept.Messaging;

namespace BulkAccept.UI.Models
{
    public class AcceptModel : ITitleCreateCommand
    {
        public string VinNo { get; set; }

        public int RefNo { get; set; }
    }
}